using Application.Exceptions;
using Application.Interfaces.Repositories;
using Application.Wrappers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.BankAccountActivity.Commands.DeleteBankAccountActivityById
{
    /// <summary>
    /// DeleteBankAccountActivityByIdCommand
    /// </summary>
    public class DeleteBankAccountActivityByIdCommand : IRequest<Response<int>>
    {
        public int Id { get; set; }

        /// <summary>
        /// DeleteBankAccountActivityByIdCommand -> Handler
        /// </summary>
        public class DeleteBankAccountActivityByIdCommandHandler : IRequestHandler<DeleteBankAccountActivityByIdCommand, Response<int>>
        {
            private readonly IBankAccountActivityRepositoryAsync _repository;

            public DeleteBankAccountActivityByIdCommandHandler(IBankAccountActivityRepositoryAsync repository)
            {
                this._repository = repository;
            }

            public async Task<Response<int>> Handle(DeleteBankAccountActivityByIdCommand command, CancellationToken cancellationToken)
            {
                var entity = await this._repository.GetByIdAsync(command.Id);

                if (entity == null) 
                    throw new ApiException(BusinessExceptions.NotFound);
                
                await this._repository.DeleteSoftAsync(entity.Id);
                
                return new Response<int>(entity.Id);
            }
        }
    }
}
