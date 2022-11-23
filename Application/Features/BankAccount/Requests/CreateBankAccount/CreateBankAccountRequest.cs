using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.BankAccount.Requests.CreateBankAccount
{
    public class CreateBankAccountRequest
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public string Number { get; set; }
        public string Iban { get; set; }

        public Nullable<DateTime> OpenDate { get; set; }
        public Nullable<decimal> TotalBalance { get; set; }
        public Nullable<decimal> TotalCreditBalance { get; set; }
        public Nullable<decimal> TotalCreditCardBalance { get; set; }
        public Nullable<decimal> BlockBalanceLimit { get; set; }
        public Nullable<decimal> BlockCreditLimit { get; set; }

        public int BankAccountTypeId { get; set; }

        public int CurrencyId { get; set; }

        public int IntegratorId { get; set; }

        public int BankId { get; set; }

        public int CustomerId { get; set; }
    }
}
