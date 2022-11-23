using Application.Currency.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.Currency.Requests.CreateCurrency
{
    public class CreateCurrencyRequest
    {
        public string Name { get; set; }

        public string Code { get; set; }
    }
}
