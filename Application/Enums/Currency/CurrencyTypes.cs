using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Currency.Enums
{
	public enum CurrencyTypes
	{
		[System.ComponentModel.Description("Unknown")]
		Unknown = -1,

		[System.ComponentModel.Description("Domestic")]
		Domestic = 1,

		[System.ComponentModel.Description("International")]
		International = 2
	}
}
