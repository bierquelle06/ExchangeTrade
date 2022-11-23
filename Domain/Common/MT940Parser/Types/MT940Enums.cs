using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Common.MT940Parser.Types
{
    public enum SwiftBalanceType
    {
        Opening,
        interim,
        Closing,
        Dated,
        Future
    }

    public enum CreditDebitIdentificationType
    {
        Unknown,

		[System.ComponentModel.Description("Alacak")]
		Credit,

		[System.ComponentModel.Description("Borç")]
		Debit,

		[System.ComponentModel.Description("Alacak İptal")]
		CancellationCredit,

		[System.ComponentModel.Description("Borç İptal")]
		CancellationDebit
    }
}