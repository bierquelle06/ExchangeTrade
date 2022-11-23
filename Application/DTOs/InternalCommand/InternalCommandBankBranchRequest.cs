using System;
using System.Collections.Generic;
using System.Text;

namespace Application.DTOs.InternalCommand
{
    public class InternalCommandBankBranchRequest
    {
        public int Id { get; set; }

        public Nullable<int> BankId { get; set; }

        public string Name { get; set; }

        public Nullable<bool> IsDelete { get; set; }

        public Nullable<System.DateTime> CreateDate { get; set; }
        public Nullable<System.DateTime> UpdateDate { get; set; }

        public virtual InternalCommandBankRequest Bank { get; set; }
    }
}
