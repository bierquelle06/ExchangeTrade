using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.Integrator.Queries.GetAllIntegrators
{
    public class GetAllIntegratorsViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Code { get; set; }

        public string Host { get; set; }

        public string Port { get; set; }

        public string Url { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }
    }
}
