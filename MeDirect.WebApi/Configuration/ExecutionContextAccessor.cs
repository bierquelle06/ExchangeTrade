using System;
using System.Linq;
using Application.Configuration;
using Microsoft.AspNetCore.Http;

namespace MeDirect.WebApi.Configuration
{
    public class ExecutionContextAccessor : IExecutionContextAccessor
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ExecutionContextAccessor(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public bool IsAvailable => _httpContextAccessor.HttpContext != null;
    }
}
