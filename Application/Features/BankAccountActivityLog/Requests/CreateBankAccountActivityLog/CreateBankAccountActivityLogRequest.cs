using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.BankAccount.Requests.CreateBankAccountActivityLog
{
    public class CreateBankAccountActivityLogRequest
    {
        public int BankAccountId { get; set; }

        public int ProcessId { get; set; }
    }
}
