using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Configuration.Commands
{
    public class CommandBase : ICommand
    {
        public int Id { get; }

        public CommandBase()
        {
            this.Id = 0;
        }

        protected CommandBase(int id)
        {
            this.Id = id;
        }
    }

    public abstract class CommandBase<TResult> : ICommand<TResult>
    {
        public int Id { get; }

        protected CommandBase()
        {
            this.Id = 0;
        }

        protected CommandBase(int id)
        {
            this.Id = id;
        }
    }
}
