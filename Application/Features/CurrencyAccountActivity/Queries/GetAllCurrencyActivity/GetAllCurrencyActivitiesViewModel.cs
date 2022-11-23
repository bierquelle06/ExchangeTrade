using Application.Features.Currency.Queries.GetAllCurrency;

using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.CurrencyActivity.Queries.GetAllCurrencyActivities
{
    public class GetAllCurrencyActivitiesViewModel
    {
        public int Id { get; set; }

        public int CurrencyId { get; }

        public string Symbol { get; }

        public decimal Rate { get; }

        public virtual GetAllCurrencyViewModel Currency { get; set; }
    }
}
