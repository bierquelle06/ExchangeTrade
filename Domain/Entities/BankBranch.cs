using Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities.BankBranch
{
    public partial class BankBranch : AuditableBaseEntity
    {
        public int BankId { get; set; }

        public string Name { get; set; }
    }
}
