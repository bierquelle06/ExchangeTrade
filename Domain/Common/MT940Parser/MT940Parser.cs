using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Common.MT940Parser.Factories;
using Domain.Common.MT940Parser.Models;

namespace Domain.Common.MT940Parser
{
    public class MT940Parser
    {
        private readonly MT940Options MT940Options;

        private delegate Transaction GetTransactionLine(Statement statement, string rawLine);

        public MT940Parser(MT940Options options)
        {
            this.MT940Options = options;
        }

        public List<Statement> Parse(string fileName)
        {
            var lines = File.ReadAllText(fileName).Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries).ToList();
            var result = new List<Statement>();
            Statement currentStatement = result.FirstOrDefault(); ;
            foreach (var line in lines)
            {
                var tag = TagFactory.Create(line);
                var command = line.Replace($":{tag}:", "");
                switch (tag)
                {
                    
                    /// Transaction reference number                     case "20":
                        currentStatement = new Statement();
                        result.Add(currentStatement);
                        currentStatement.TransactionReference = command;
                        break;
                    /// Transaction reference number Related reference
                    case "21": currentStatement.RelatedMessage = command; break;
                    ///Account Identification
                    case "25": currentStatement.Account = command; break;

                    /// Statement Number / Sequence Number                    case "28":
                    case "28c":
                    case "28C":
                        currentStatement.StatementNumber = GetStatementNumber(command);
                        currentStatement.SequenceNumber = GetSequenceNumber(command); break;
                    /// Opening Balance
                    case "60m":
                    case "60f":
                    case "60F":
                    case "60M":
                        currentStatement.OpeningBalance = new TransactionBalance(command, MT940Options);
                        break;
                    /// Statement Line
                    /*
                     * Value Date
                     * Entry Date
                     * (Reversed) Debit / Credit Mark
                     * Funds code
                     * Amount
                     * Transaction Type Identification Code
                     * Reference for the Account Owner
                     * Account Servicing Institution’s Reference
                     * Supplementary Details
                     */
                    case "61":
                        currentStatement.Transactions.Add(new Transaction(currentStatement, command, currentStatement.OpeningBalance.Currency, MT940Options));
                        break;
                    /// Information to Account Owner
                    case "86":
                        //burada parse yapmak gerekecek
                        currentStatement.Transactions.Last().Description = command; break;
                    /// Closing Balance
                    case "62f":
                    case "62F":
                        currentStatement.ClosingAvailableBalance = new TransactionBalance(command, MT940Options);
                        break;

                    case "62m":
                    case "62M":
                        currentStatement.ClosingAvailableBalance = new TransactionBalance(command, MT940Options);
                        break;
                    /// Closing available balance
                    case "64":
                        currentStatement.ClosingAvailableBalance = new TransactionBalance(command, MT940Options);
                        break;
                    /// Forward value balance
                    case "65":
                        currentStatement.ForwardAvailableBalance = (new TransactionBalance(command, MT940Options));
                        break;
                    default:
                        throw new Exception("code tanınamadı");
                }
            }
            return result;
        }

        private int GetSequenceNumber(string line)
        {
            var transaction = line.Split('/');
            var sequenceNumber = 0;

            if (transaction.Length > 1 && int.TryParse(transaction[1], out sequenceNumber))
                return sequenceNumber;
            return 0;
        }

        private int GetStatementNumber(string line)
        {
            var transaction = line.Split('/');
            var statementNumber = 0;
            if (int.TryParse(transaction[0], out statementNumber))
                return statementNumber;
            return 0;
        }
    }
}