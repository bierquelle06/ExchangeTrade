using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.Bank.Requests.UpdateBank
{
    public class UpdateBankRequest
    {
        public int BranchId { get; }

        public string UUID { get; }

        public string Name { get; }

        public string Code { get; }
    }
}
