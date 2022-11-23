using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Domain.Common.MT940Parser.Factories;
using Domain.Common.MT940Parser.Types;

namespace Domain.Common.MT940Parser.Models
{
    /// <summary>
    ///
    /// </summary>
    [Serializable]
    public class TransactionBalance
    {
        /// <summary>
        ///
        /// </summary>
        public CreditDebitIdentificationType DebitCredit { get; private set; }

        /// <summary>
        ///
        /// </summary>
        public DateTime EntryDate { get; private set; }

        /// <summary>
        ///
        /// </summary>
        public string Currency { get; private set; }

        /// <summary>
        ///
        /// </summary>
        public Decimal Balance { get; private set; }

        public TransactionBalance(string data, MT940Options options)
        {
            if (String.IsNullOrWhiteSpace(data))
            {
                throw new ArgumentException("data can not be empty", data);
            }

            var regex = new Regex(@"([C|D]{1})([0-9]{6})([A-Z]{3})(\d.*)");
            var match = regex.Match(data);

            if (match.Groups.Count < 3)
                throw new System.Data.InvalidExpressionException(data);

            this.DebitCredit = DebitCreditFactory.Create(match.Groups[1].Value);

            this.EntryDate = DateTime.ParseExact(match.Groups[2].Value, options.BalanceDateTimeFormat, CultureInfo.InvariantCulture);

            this.Currency = (match.Groups[3].Value);
            this.Balance = Convert.ToDecimal(match.Groups[4].Value, options.CultureInfo);
        }

        /// <summary>
        /// Returns the string represantion of the transaction balance.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return this.Balance.ToString();
        }
    }
}