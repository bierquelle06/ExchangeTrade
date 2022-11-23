using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Configuration.Commands
{
    public interface ICommand : IRequest
    {
        int Id { get; }
    }

    public interface ICommand<out TResult> : IRequest<TResult>
    {
        int Id { get; }
    }
}
