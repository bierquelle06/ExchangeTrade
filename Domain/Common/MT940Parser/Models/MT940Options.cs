using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text.RegularExpressions;

namespace Domain.Common.MT940Parser.Models
{
    public delegate int YearLengthDecider(Statement statement, Transaction transaction);

    public class MT940Options
    {
        public string TransactionRegEx { get; set; }
        public string BalanceDateTimeFormat { get; set; }

        public CultureInfo CultureInfo { get; set; }
        public Dictionary<string, string> FundsCode { get; set; }

        public Func<Statement, Transaction, int> GetYearLength;
    }
}