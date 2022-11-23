using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Common
{
    /// <summary>
    /// Convert To Decimal
    /// </summary>
    public class NumberExtensions
    {
        private static CultureInfo resourceCulture;

        /// <summary>
        /// ConvertNonNumericToString
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string ConvertNonNumericToString(decimal value)
        {
            string result = "";

            try
            {
                if (value < 0)
                    value = value * -1;

                result = (new string(value.ToString().Where(c => char.IsDigit(c)).ToArray()));
            }
            catch
            {
                result = "";
            }

            return result;
        }

        /// <summary>
        /// Convert string value to decimal ignore the culture.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>Decimal value.</returns>
        public static decimal ConvertToDecimal(string value = "", string bankName = "MEDIRECTBANK", CultureInfo cultureInfo = null)
        {
            value = string.IsNullOrEmpty(value) == true ? "0" : value;

            resourceCulture = cultureInfo ?? CultureInfo.InvariantCulture;

            int changeValue = 1;

            if (value.Contains("+"))
            {
                changeValue = 1;
                value = value.Replace("+", "");
            }

            if (value.Contains("-"))
            {
                changeValue = -1;
                value = value.Replace("-", "");
            }

            decimal number;
            string tempValue = value;

            var punctuation = value.Where(x => char.IsPunctuation(x)).Distinct();
            int count = punctuation.Count();

            NumberFormatInfo format = resourceCulture.NumberFormat;
            switch (count)
            {
                case 0:
                    break;
                case 1:
                    tempValue = value.Replace(",", ".");
                    break;
                case 2:
                    if (punctuation.ElementAt(0) == '.')
                        tempValue = SwapChar(value, '.', ',');
                    break;
                default:
                    throw new InvalidCastException();
            }

            number = changeValue * decimal.Parse(tempValue, NumberStyles.Currency | NumberStyles.AllowCurrencySymbol | NumberStyles.AllowDecimalPoint | NumberStyles.AllowThousands, format);

            return number;
        }

        /// <summary>
        /// Swaps the char.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="from">From.</param>
        /// <param name="to">To.</param>
        /// <returns></returns>
        public static string SwapChar(string value, char from, char to)
        {
            if (value == null)
                throw new ArgumentNullException("value");

            StringBuilder builder = new StringBuilder();

            foreach (var item in value)
            {
                char c = item;
                if (c == from)
                    c = to;
                else if (c == to)
                    c = from;

                builder.Append(c);
            }

            return builder.ToString();
        }
    }
}
