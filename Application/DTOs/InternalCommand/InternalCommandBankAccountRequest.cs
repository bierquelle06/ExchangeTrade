using System;
using System.Collections.Generic;
using System.Text;

namespace Application.DTOs.InternalCommand
{
    public class InternalCommandBankAccountRequest
    {
        public InternalCommandBankAccountRequest()
        {
            this.BankAccountActivities = new HashSet<InternalCommandBankAccountActivityRequest>();
        }

        public int Id { get; set; }
        
        public Nullable<int> CustomerId { get; set; }

        public Nullable<int> BankAccountTypeId { get; set; }
        public Nullable<int> CurrencyId { get; set; }
        public Nullable<int> IntegratorId { get; set; }
        public Nullable<int> BankId { get; set; }

        public Nullable<System.DateTime> OpenDate { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string Number { get; set; }
        public string Iban { get; set; }

        public Nullable<decimal> TotalBalance { get; set; }
        public Nullable<decimal> TotalCreditBalance { get; set; }
        public Nullable<decimal> TotalCreditCardBalance { get; set; }
        public Nullable<decimal> BlockBalanceLimit { get; set; }
        public Nullable<decimal> BlockCreditLimit { get; set; }

        public Nullable<bool> IsDelete { get; set; }
        
        public Nullable<System.DateTime> CreateDate { get; set; }
        public Nullable<System.DateTime> UpdateDate { get; set; }
        public Nullable<System.DateTime> DeleteDate { get; set; }

        public virtual InternalCommandBankAccountTypeRequest BankAccountType { get; set; }
        public virtual InternalCommandCurrencyRequest Currency { get; set; }
        public virtual InternalCommandIntegratorRequest Integrator { get; set; }
        public virtual InternalCommandBankRequest Bank { get; set; }

        public virtual ICollection<InternalCommandBankAccountActivityRequest> BankAccountActivities { get; set; }
    }
}
