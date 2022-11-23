using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Common
{
    public abstract class AuditableBaseEntity : BaseEntity
    {
        public Nullable<bool> IsDelete { get; set; }

        public Nullable<DateTime> CreateDate { get; set; }
        public Nullable<DateTime> UpdateDate { get; set; }
        public Nullable<DateTime> DeleteDate { get; set; }
    }
}
