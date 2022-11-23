using Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities.Currency
{
    public partial class Currency : AuditableBaseEntity
    {
        public string Name { get; set; }

        public string Code { get; set; }
    }
}
