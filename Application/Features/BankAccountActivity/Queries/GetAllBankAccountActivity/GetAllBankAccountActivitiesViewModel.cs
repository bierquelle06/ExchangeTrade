using Application.Features.BankAccount.Queries.GetAllBankAccounts;

using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.BankAccountActivity.Queries.GetAllBankAccountActivities
{
    public class GetAllBankAccountActivitiesViewModel
    {
        public int Id { get; set; }

        public int BankAccountId { get; }

        public short Type { get; }

        public string CurrencyCode { get; }
        public string TrxCode_01 { get; }
        public string TrxCode_02 { get; }

        public string Description { get; }
        public string Source { get; }
        public string ProcessID { get; }
        public string ProcessName { get; }

        public Nullable<DateTime> ProcessDate { get; }

        public Nullable<decimal> Quantity { get; }
        public Nullable<decimal> Balance { get; }

        public string OtherSideVKN { get; }
        public string OtherAccNo { get; }
        public string OtherSideIBAN { get; }

        public string ReceiptNo { get; }

        public bool IsTransfer { get; }

        public string Transfer { get; }

        public string Note { get; }

        public virtual GetAllBankAccountsViewModel BankAccount { get; set; }
    }
}
