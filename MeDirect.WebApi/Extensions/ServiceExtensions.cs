using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MeDirect.WebApi.Extensions
{
    public static class ServiceExtensions
    {
        private const string apiName = "MeDirect API";

        private const int apiMajorVersion = 1;
        private const int apiMinorVersion = 0;

        private const string apiEmailContact = "info@medirect.com";
        private const string apiUrl = "https://www.medirect.com.mt";

        private const string apiXml = "MeDirect.WebApi.xml";

        public static void AddSwaggerExtension(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                string xmlPath = System.AppDomain.CurrentDomain.BaseDirectory + "\\" + apiXml;

                c.IncludeXmlComments(xmlPath);
                c.SwaggerDoc("v" + apiMajorVersion, new OpenApiInfo
                {
                    Title = apiName,
                    Version = $"v{apiMajorVersion}.{apiMinorVersion}",
                    Description = $"{apiName} v{apiMajorVersion}.{apiMinorVersion} This Api will be responsible for overall data distribution and authorization.",
                    Contact = new OpenApiContact
                    {
                        Name = apiName,
                        Email = apiEmailContact,
                        Url = new Uri(apiUrl),
                    }
                });

                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    Description = "Input your Bearer token in this format - Bearer {your token here} to access this API. Example: \"Bearer {token}\"",
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer",
                            },
                            Scheme = "Bearer",
                            Name = "Bearer",
                            In = ParameterLocation.Header,
                        }, new List<string>()
                    },
                });
            });
        }

        public static void AddApiVersioningExtension(this IServiceCollection services)
        {
            services.AddApiVersioning(config =>
            {
                // Specify the default API Version as 1.0
                config.DefaultApiVersion = new ApiVersion(apiMajorVersion, apiMinorVersion);
                // If the client hasn't specified the API version in the request, use the default API version number 
                config.AssumeDefaultVersionWhenUnspecified = true;
                // Advertise the API versions supported for the particular endpoint
                config.ReportApiVersions = true;
            });
        }

        public static void UseSwaggerDocumentation(this IApplicationBuilder app)
        {
            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint($"/swagger/v{apiMajorVersion}/swagger.json", $"{apiName} v{apiMajorVersion}.{apiMinorVersion}");
            });
        }
    }
}
