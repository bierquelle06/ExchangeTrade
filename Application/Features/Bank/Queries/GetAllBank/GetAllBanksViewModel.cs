using Application.Features.BankBranch.Queries.GetAllBankBranchs;

using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.Bank.Queries.GetAllBanks
{
    public class GetAllBanksViewModel
    {
        public int Id { get; set; }

        public string UUID { get; set; }

        public string Name { get; set; }

        public string Code { get; set; }

        public virtual GetAllBankBranchsViewModel Branch { get; set; }
    }
}
