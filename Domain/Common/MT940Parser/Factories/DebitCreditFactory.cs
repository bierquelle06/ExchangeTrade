using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Common.MT940Parser.Types;

namespace Domain.Common.MT940Parser.Factories
{
    public static class DebitCreditFactory
    {
        public static CreditDebitIdentificationType Create(string type)
        {
            switch (type)
            {
                case "D":
                    return CreditDebitIdentificationType.Debit;

                case "C":
                    return CreditDebitIdentificationType.Credit;

                case "RC":
                    return CreditDebitIdentificationType.CancellationCredit;

                case "RD":
                    return CreditDebitIdentificationType.CancellationDebit;
            }

            throw new ArgumentException(type, nameof(type));
        }
    }
}