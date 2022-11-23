using Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities.InternalCommand
{
    public partial class InternalCommand : AuditableBaseEntity
    {
        public string Name { get; set; }

        public DateTime EnqueueDate { get; set; }

        public string Type { get; set; }

        public string Data { get; set; }

        public DateTime? ProcessedDate { get; set; }
    }
}
