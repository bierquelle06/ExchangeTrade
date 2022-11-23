using Application;
using Application.Interfaces;
using Application.Configuration.Quartz;
using Application.Configuration;

using Infrastructure.Identity;
using Infrastructure.Persistence;
using Infrastructure.Shared;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Http;

using Serilog;
using Serilog.Formatting.Compact;

using Infrastructure.Persistence.Processing.InternalCommands;
using MediatR;

using MeDirect.WebApi.Extensions;
using MeDirect.WebApi.Services;
using MeDirect.WebApi.Configuration;

using Domain.Settings;

using Application.Wrappers;

namespace MeDirect.WebApi
{
    public class Startup
    {
        public IConfiguration _config { get; }

        private static ILogger _logger;

        public Startup(IConfiguration configuration)
        {
            _logger = ConfigureLogger();
            _logger.Information("Logger configured");

            _config = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            //services.AddControllers();

            services.AddControllers(config =>
            {
                config.Filters.Add(new WebApiActionFilter(_config));
                config.Filters.Add(new WebApiControllerFilter(_config));
            });

            services.AddScoped<WebApiActionFilter>();
            services.AddScoped<WebApiControllerFilter>();

            services.AddMemoryCache();

            services.AddApplicationLayer();
            services.AddIdentityInfrastructure(_config);
            services.AddPersistenceInfrastructure(_config);
            services.AddSharedInfrastructure(_config);

            services.AddSwaggerExtension();
            services.AddApiVersioningExtension();

            services.AddHealthChecks();

            services.AddScoped<IAuthenticatedUserService, AuthenticatedUserService>();

            services.AddHttpContextAccessor();

            ServiceProvider serviceProvider = services.BuildServiceProvider();

            IExecutionContextAccessor executionContextAccessor = new ExecutionContextAccessor(serviceProvider.GetService<IHttpContextAccessor>());

            var quartzSettings = _config.GetSection("QuartzSettings").Get<QuartzSettings>();
            if (quartzSettings.IsActive)
            {
                _logger.Information("Quartz service is an actived!");

                services.Configure<InternalCommandSettings>(_config.GetSection("InternalCommandSettings"));

                services.AddTransient<ProcessInternalCommandsJob>();
                services.AddScoped<IRequestHandler<ProcessInternalCommand, Response<bool>>, ProcessInternalCommandHandler>();
                services.AddMediatR(typeof(ProcessInternalCommand).GetType().Assembly);
                
                var responseService = ApplicationStartup.UseQuartz(services, _logger, quartzSettings, executionContextAccessor);
                
                if(responseService == null)
                    _logger.Information("Quartz service is not configured!");
            }
            else
                _logger.Information("Quartz service is not an activated!");
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseSwaggerDocumentation();
            app.UseErrorHandlingMiddleware();
            app.UseHealthChecks("/health");

            app.UseEndpoints(endpoints =>
             {
                 endpoints.MapControllers();
             });
        }

        private static ILogger ConfigureLogger()
        {
            return new LoggerConfiguration()
                .Enrich.FromLogContext()
                .WriteTo.Console(outputTemplate: "[{Timestamp:HH:mm:ss} {Level:u3}] [{Context}] {Message:lj}{NewLine}{Exception}")
                .WriteTo.RollingFile(new CompactJsonFormatter(), "logs/logs")
                .CreateLogger();
        }
    }
}
