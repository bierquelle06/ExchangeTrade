using Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities.BankAccountActivity
{
    public partial class BankAccountActivity : AuditableBaseEntity
    {
        public Nullable<decimal> Quantity { get; set; }
        public string CurrencyCode { get; set; }
        public string Description { get; set; }
        public string Source { get; set; }
        public int ProcessID { get; set; }
        public string ProcessName { get; set; }
        public Nullable<DateTime> ProcessDate { get; set; }
        public Nullable<decimal> Balance { get; set; }
        public string ReceiptNo { get; set; }
        public string Note { get; set; }

        public int BankAccountId { get; set; }
    }
}
