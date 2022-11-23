using System;
using System.Data;
using System.Globalization;
using System.Text.RegularExpressions;
using Domain.Common.MT940Parser.Factories;
using Domain.Common.MT940Parser.Types;

namespace Domain.Common.MT940Parser.Models
{
    public class Transaction
    {
        private readonly string line;
        public string Currency { get; set; }
        private readonly MT940Options options;

        /// <summary>
        /// Unparsed raw data.
        /// Code 61.
        /// </summary>
        public string Value { get; set; }

        /// <summary>
        /// Description of the transaction.
        /// Code 86
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// ??
        /// </summary>
        public DateTime ValueDate { get; private set; }

        /// <summary>
        /// Optional date.
        /// </summary>
        public DateTime? EntryDate { get; private set; }

        /// <summary>
        /// ??
        /// </summary>
        public String FundsCode { get; set; }

        /// <summary>
        /// Transaction amount
        /// </summary>
        public Decimal Amount { get; private set; }

        /// <summary>
        /// Transaction type, a value that starts with N and is followed by 3 numbers.
        /// </summary>
        public String TransactionType { get; private set; }

        public string TransactionExplaining { get; private set; }

        /// <summary>
        /// NONREF or account number of the other party.
        /// </summary>
        public String Reference { get; private set; }

        /// <summary>
        /// Debit-credit indication
        /// </summary>
        public CreditDebitIdentificationType DebitCredit { get; private set; }

        /// <summary>
        /// Description details (see MT940Params)
        /// </summary>
        public TransactionDetails Details { get; set; }

        /// <summary>
        /// Subfield 9 Supplementary Details (optional)
        /// </summary>
        public string SupplementaryDetails { get; private set; }

        /// <summary>
        /// Subfield 8 [//16x] (Reference of the Account Servicing Institution)
        /// </summary>
        public string AccountServicingReference { get; private set; }

        public Transaction(Statement statement, string line, string currency, MT940Options options)
        {
            this.line = line;
            this.Currency = currency;
            this.options = options;
            var yearlength = options.GetYearLength(statement, this);
            var regex = new Regex(@"^(?<valuedate>(?<year>\d{" + yearlength.ToString() + @"})(?<month>\d{2})(?<day>\d{2}))(?<entrydate>\d{0,4})?(?<creditdebit>C|D|RC|RD)(?<fundscode>[A-z]{0,1}?)(?<ammount>\d*[,.]\d{0,2})(?<transactiontype>[\w\s]{4})(?<reference>[\s\w]{0,16})(?:(?<servicingreference>//[\s\w]{0,16}))*(?<supplementary>\r\n[\s\w]{0,34})*");

            var match = regex.Match(line);

            if (!match.Success)
            {
                throw new InvalidExpressionException(line);
            }

            // Raw line.
            Value = line;

            Details = new TransactionDetails();
            ValueDate = DateTime.ParseExact(match.Groups["valuedate"].Value, yearlength == 2 ? "yyMMdd" : "yyyyMMdd", CultureInfo.InvariantCulture);
            if (!String.IsNullOrEmpty(match.Groups["entrydate"].Value))
                EntryDate = DateTime.ParseExact(match.Groups["entrydate"].Value, "MMdd", CultureInfo.InvariantCulture);
            DebitCredit = DebitCreditFactory.Create(match.Groups["creditdebit"].Value);

            FundsCode = match.Groups["fundscode"].Value;

            Amount = Decimal.Parse(match.Groups["ammount"].Value, options.CultureInfo);

            TransactionType = match.Groups["transactiontype"].Value.Substring(1);
            TransactionExplaining = ExplainFactory.Create(TransactionType, DebitCredit);
            Reference = match.Groups["reference"].Value;

            AccountServicingReference = match.Groups["servicingreference"].Value.Trim("//".ToCharArray());
            SupplementaryDetails = match.Groups["supplementary"].Value.Trim("\r\n".ToCharArray());
        }
    }
}