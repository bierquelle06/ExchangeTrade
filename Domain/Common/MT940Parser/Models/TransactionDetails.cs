using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Common.MT940Parser.Models
{
    /// <summary>
    /// Details for transaction field :86: (transaction description)
    /// </summary>
    public class TransactionDetails
    {
        /// <summary>
        /// Transaction account - IBAN
        /// </summary>
        public string Account { get; set; }

        /// <summary>
        /// Company name, address
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Operation description
        /// </summary>
        public string Description { get; set; }
    }
}