using Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities.Bank
{
    public partial class Bank : AuditableBaseEntity
    {
        public string Name { get; set; }
    }
}
