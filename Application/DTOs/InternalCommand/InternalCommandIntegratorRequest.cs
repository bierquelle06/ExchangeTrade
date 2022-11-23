using System;
using System.Collections.Generic;
using System.Text;

namespace Application.DTOs.InternalCommand
{
    public class InternalCommandIntegratorRequest
    {
        public InternalCommandIntegratorRequest()
        {
            this.BankAccounts = new HashSet<InternalCommandBankAccountRequest>();
        }

        public int Id { get; set; }

        public string Name { get; set; }
        public string Code { get; set; }
        public string Host { get; set; }
        public string Port { get; set; }
        public string Url { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }

        public Nullable<short> Type { get; set; }
        public Nullable<bool> IsDelete { get; set; }

        public Nullable<System.DateTime> CreateDate { get; set; }
        public Nullable<System.DateTime> UpdateDate { get; set; }
        public Nullable<System.DateTime> DeleteDate { get; set; }

        public virtual ICollection<InternalCommandBankAccountRequest> BankAccounts { get; set; }
    }
}
