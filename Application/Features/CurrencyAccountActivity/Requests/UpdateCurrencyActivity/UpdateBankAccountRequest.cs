using Application.BankActivity.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.CurrencyActivity.Requests.UpdateCurrencyActivity
{
    public class UpdateCurrencyActivityRequest
    {
        public int BankAccountId { get; set; }

        public int CurrencyId { get; set; }
        public string Symbol { get; set; }
        public decimal Rate { get; set; }
    }
}
