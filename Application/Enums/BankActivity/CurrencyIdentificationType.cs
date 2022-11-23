using System;
using System.Collections.Generic;
using System.Text;

namespace Application.BankActivity.Enums
{
    public enum CurrencyIdentificationType
    {
		[System.ComponentModel.Description("Unknown")]
		Unknown = -1,

		[System.ComponentModel.Description("Buy")]
		Buy = 1,

		[System.ComponentModel.Description("Sell")]
		Sell = 2
	}
}
