using Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities.BankAccount
{
    public partial class BankAccount : AuditableBaseEntity
    {
        public Nullable<int> CustomerId { get; set; }
        public Nullable<int> BankId { get; set; }
        public Nullable<int> CurrencyId { get; set; }
        public Nullable<int> IntegratorId { get; set; }
        public Nullable<int> BankAccountTypeId { get; set; }
        public Nullable<DateTime> OpenDate { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string Iban { get; set; }
        public string Number { get; set; }
        public Nullable<decimal> TotalBalance { get; set; }
        public Nullable<decimal> TotalCreditBalance { get; set; }
        public Nullable<decimal> TotalCreditCardBalance { get; set; }
        public Nullable<decimal> BlockBalanceLimit { get; set; }
        public Nullable<decimal> BlockCreditLimit { get; set; }
    }
}
