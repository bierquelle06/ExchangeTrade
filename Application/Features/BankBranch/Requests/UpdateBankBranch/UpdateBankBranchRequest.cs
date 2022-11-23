using Application.Branch.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.BankBranch.Requests.UpdateBankBranch
{
    public class UpdateBankBranchRequest
    {
        public string Name { get; set; }

        public int BankId { get; set; }
    }
}
