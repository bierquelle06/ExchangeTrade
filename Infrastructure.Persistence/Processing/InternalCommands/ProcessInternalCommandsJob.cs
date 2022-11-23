using MediatR;
using Microsoft.Extensions.Logging;
using Quartz;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistence.Processing.InternalCommands
{
    [DisallowConcurrentExecution]
    public class ProcessInternalCommandsJob : IJob
    {
        private readonly IMediator _mediator;
        private readonly ILogger<ProcessInternalCommandsJob> _logger;
        public ProcessInternalCommandsJob(IMediator mediator, ILogger<ProcessInternalCommandsJob> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            try
            {
                _logger.LogInformation($"ProcessInternalCommandsJob::Executed: {DateTime.Now}");

                await _mediator.Send(new ProcessInternalCommand() { 
                    Name = "Integrator Command",
                    Code = "INTCMD"
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "ProcessInternalCommandsJob::Execute Error");
            }
        }
    }
}
