using System;
using System.Collections.Generic;
using System.Text;

namespace Application.DTOs.InternalCommand
{
    public class InternalCommandBankAccountActivityRequest
    {
        public int Id { get; set; }

        public int BankAccountId { get; set; }

        public Nullable<decimal> Quantity { get; set; }
        
        public string CurrencyCode { get; set; }
        
        public string Description { get; set; }
        public string Source { get; set; }
        
        public int ProcessID { get; set; }
        public string ProcessName { get; set; }
        
        public Nullable<System.DateTime> ProcessDate { get; set; }
        public Nullable<decimal> Balance { get; set; }
        
        public string ReceiptNo { get; set; }
        
        public string Note { get; set; }
        
        public Nullable<bool> IsDelete { get; set; }

        public Nullable<System.DateTime> CreateDate { get; set; }
        public Nullable<System.DateTime> UpdateDate { get; set; }
        public Nullable<System.DateTime> DeleteDate { get; set; }

        public virtual InternalCommandBankAccountRequest BankAccount { get; set; }

    }
}
