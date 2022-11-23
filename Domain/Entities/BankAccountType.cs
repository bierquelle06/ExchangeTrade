using Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities.BankAccountType
{
    public partial class BankAccountType : AuditableBaseEntity
    {
        public string Name { get; set; }
    }
}
