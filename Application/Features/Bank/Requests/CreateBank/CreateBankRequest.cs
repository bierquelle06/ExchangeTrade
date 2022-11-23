using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.Bank.Requests.CreateBank
{
    public class CreateBankRequest
    {
        public string Name { get; set; }

        public string Code { get; set; }

        public string UUID { get; set; }

        public int BranchId { get; set; }
    }
}
