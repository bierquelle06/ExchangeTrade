using Application.Configuration;
using Application.Configuration.Quartz;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Autofac.Extras.CommonServiceLocator;
using CommonServiceLocator;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Quartz;
using Quartz.Impl;
using System;
using System.Collections.Generic;
using System.Text;
using Serilog;
using Infrastructure.Persistence.Quartz;
using Infrastructure.Persistence.Processing.InternalCommands;

namespace Infrastructure.Persistence
{
    public class ApplicationStartup
    {
        public static IServiceProvider UseQuartz(
            IServiceCollection services,
            ILogger logger,
            QuartzSettings quartzSettings,
            IExecutionContextAccessor executionContextAccessor)
        {
            var serviceProvider = StartQuartz(services, logger, executionContextAccessor);

            return serviceProvider;
        }

        private static IServiceProvider StartQuartz(IServiceCollection services, ILogger logger, IExecutionContextAccessor executionContextAccessor)
        {
            var schedulerFactory = new StdSchedulerFactory();
            var scheduler = schedulerFactory.GetScheduler().GetAwaiter().GetResult();

            try
            {
                var container = new ContainerBuilder();

                container.Populate(services);

                container.RegisterModule(new QuartzModule());

                container.RegisterInstance(executionContextAccessor);

                var buildContainer = container.Build();

                scheduler.JobFactory = new JobFactory(buildContainer);

                scheduler.Start().GetAwaiter().GetResult();

                var processInternalCommandsJob = JobBuilder.Create<ProcessInternalCommandsJob>().Build();
                var triggerCommandsProcessing =
                    TriggerBuilder
                        .Create()
                        .StartNow()
                        .WithCronSchedule("0/15 * * ? * *")
                        .Build();

                scheduler.ScheduleJob(processInternalCommandsJob, triggerCommandsProcessing).GetAwaiter().GetResult();

                ServiceLocator.SetLocatorProvider(() => new AutofacServiceLocator(buildContainer));

                var serviceProvider = new AutofacServiceProvider(buildContainer);

                CompositionRoot.SetContainer(buildContainer);

                return serviceProvider;
            }
            catch (Exception ex)
            {
                logger.Error(ex, $"StartQuartz::Error Detail : {ex.Message}");

                return null;
            }
        }
    }
}
