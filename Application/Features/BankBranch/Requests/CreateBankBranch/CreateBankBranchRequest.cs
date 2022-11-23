using Application.Branch.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.BankBranch.Requests.CreateBankBranch
{
    public class CreateBankBranchRequest
    {
        public string Name { get; set; }

        public int BankId { get; set; }
    }
}
