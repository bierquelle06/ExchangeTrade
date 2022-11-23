using System;
using System.Collections.Generic;
using System.Text;

namespace Application.DTOs.InternalCommand
{
    public class InternalCommandCurrencyRequest
    {
        public InternalCommandCurrencyRequest()
        {
            this.BankAccounts = new HashSet<InternalCommandBankAccountRequest>();
        }

        public int Id { get; set; }

        public string Name { get; set; }
        public string Code { get; set; }

        public Nullable<bool> IsDelete { get; set; }
        
        public Nullable<System.DateTime> CreateDate { get; set; }
        public Nullable<System.DateTime> UpdateDate { get; set; }
        public Nullable<System.DateTime> DeleteDate { get; set; }

        public virtual ICollection<InternalCommandBankAccountRequest> BankAccounts { get; set; }
    }
}
