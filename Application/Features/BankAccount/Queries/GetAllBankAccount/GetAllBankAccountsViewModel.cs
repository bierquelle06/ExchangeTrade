using Application.Features.Currency.Queries.GetAllCurrency;
using Application.Features.Integrator.Queries.GetAllIntegrators;
using Application.Features.BankAccountType.Queries.GetAllBankAccountTypes;
using Application.Features.Bank.Queries.GetAllBanks;


using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.BankAccount.Queries.GetAllBankAccounts
{
    public class GetAllBankAccountsViewModel
    {
        public int Id { get; set; }

        public string UUID { get; set; }

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

        public virtual GetAllCurrencyViewModel Currency { get; set; }

        public virtual GetAllIntegratorsViewModel Integrator { get; set; }

        public virtual GetAllBankAccountTypesViewModel BankAccountType { get; set; }

        public virtual GetAllBanksViewModel Bank { get; set; }
    }
}
