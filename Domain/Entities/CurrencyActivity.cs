using Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities.CurrencyActivity
{
    public partial class CurrencyActivity : AuditableBaseEntity
    {
        public int CurrencyId { get; set; }
        public string Symbol { get; set; }
        public string SymbolBase { get; set; }
        public string Date { get; set; }
        public int TimeStamp { get; set; }
        public Nullable<decimal> Rate { get; set; }
    }
}