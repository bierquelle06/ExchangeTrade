using System;
using System.Collections.Generic;
using System.Text;

namespace Application.DTOs.InternalCommand
{
    public class InternalCommandCurrencyActivityRequest
    {
        public int Id { get; set; }

        public int CurrencyId { get; set; }

        public string SymbolBase { get; set; }

        public string Symbol { get; set; }
        
        public string Rate { get; set; }

        public string Date { get; set; }

        public int Timestamp { get; set; }
    }
}
