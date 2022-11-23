using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Configuration.Commands
{
    public abstract class InternalCommandBase : ICommand
    {
        public int Id { get; }

        protected InternalCommandBase(int id)
        {
            this.Id = id;
        }
    }

    public abstract class InternalCommandBase<TResult> : ICommand<TResult>
    {
        public int Id { get; }

        protected InternalCommandBase()
        {
            this.Id = 0;
        }

        protected InternalCommandBase(int id)
        {
            this.Id = id;
        }
    }
}
