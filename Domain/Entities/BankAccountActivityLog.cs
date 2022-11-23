using Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities.BankAccountActivityLog
{
    public partial class BankAccountActivityLog : AuditableBaseEntity
    {
        public int BankAccountId { get; set; }
        public int ProcessId { get; set; }
    }
}
