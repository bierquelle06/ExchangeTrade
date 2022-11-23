using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Common.MT940Parser.Models
{
    public class Statement
    {
        public string TransactionReference { get; internal set; }
        public string RelatedMessage { get; internal set; }
        public string Account { get; internal set; }
        public int StatementNumber { get; internal set; }
        public int SequenceNumber { get; internal set; }
        public TransactionBalance ClosingAvailableBalance { get; internal set; }
        public List<Transaction> Transactions { get; internal set; }
        public TransactionBalance ForwardAvailableBalance { get; internal set; }
        public TransactionBalance OpeningBalance { get; internal set; }

        public Statement()
        {
            Transactions = new List<Transaction>();
        }
    }
}