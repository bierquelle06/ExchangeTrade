using Application.Branch.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.BankBranch.Queries.GetAllBankBranchs
{
    public class GetAllBankBranchsViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Code { get; set; }

        public short? Type { get; set; }
    }
}
