using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Configuration
{
    public interface IExecutionContextAccessor
    {
        bool IsAvailable { get; }
    }
}
