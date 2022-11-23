using Infrastructure.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Quartz;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace MeDirect.WebApi.Controllers
{
    public class MetaController : BaseApiController
    {
        private readonly ISchedulerFactory _factory;
        private readonly ILogger<MetaController> _logger;
        //private readonly QuartzHostedService _quartzHostedService;

        public MetaController(ISchedulerFactory factory, ILogger<MetaController> logger)
        {
            this._factory = factory;
            this._logger = logger;
            //this._quartzHostedService = quartzHostedService;
        }

        [HttpGet("/info")]
        public ActionResult<string> Info()
        {
            var assembly = typeof(Startup).Assembly;

            var lastUpdate = System.IO.File.GetLastWriteTime(assembly.Location);
            var version = FileVersionInfo.GetVersionInfo(assembly.Location).ProductVersion;

            return Ok($"Version: {version}, Last Updated: {lastUpdate}");
        }

        [HttpPut("/job/{jobCommand}")]
        public async Task<IActionResult> JobCommand(string jobCommand)
        {
            IScheduler scheduler = await this._factory.GetScheduler();

            if(string.IsNullOrEmpty(jobCommand))
            {
                return BadRequest("Please use conditions : START or STOP");
            }

            string messageResult = "";

            if (jobCommand == "START")
            {
                await scheduler.Start();

                messageResult = "started.";
            }

            if (jobCommand == "STOP")
            {
                await scheduler.Shutdown();

                messageResult = "stopped.";
            }
            
            return Ok($"Job Command: {jobCommand}. Your Job was {messageResult}");
        }
    }
}
