using System;
using System.Collections.Generic;
using System.Text;

namespace Application.DTOs.InternalCommand
{
    public class InternalCommandRequest
    {
        public virtual InternalCommandBankAccountRequest BankAccount { get; set; }
    }
}
