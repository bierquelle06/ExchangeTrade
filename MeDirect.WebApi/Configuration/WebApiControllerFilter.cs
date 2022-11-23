using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http.Controllers;

namespace MeDirect.WebApi.Configuration
{
    public class WebApiControllerFilter : IAsyncActionFilter
    {
        private readonly IConfiguration _config;

        public WebApiControllerFilter(IConfiguration config)
        {
            _config = config;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var confInternalResult = _config.GetValue<bool>("UseInInternalApp");

            // execute any code before the action executes
            var result = await next();
            // execute any code after the action executes
        }
    }

}
