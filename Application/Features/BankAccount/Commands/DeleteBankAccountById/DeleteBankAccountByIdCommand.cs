using Application.Exceptions;
using Application.Interfaces.Repositories;
using Application.Wrappers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.BankAccount.Commands.DeleteBankAccountById
{
    /// <summary>
    /// DeleteBankAccountByIdCommand
    /// </summary>
    public class DeleteBankAccountByIdCommand : IRequest<Response<int>>
    {
        public int Id { get; set; }

        /// <summary>
        /// DeleteBankAccountByIdCommand -> Handler
        /// </summary>
        public class DeleteBankAccountByIdCommandHandler : IRequestHandler<DeleteBankAccountByIdCommand, Response<int>>
        {
            private readonly IBankAccountRepositoryAsync _repository;

            public DeleteBankAccountByIdCommandHandler(IBankAccountRepositoryAsync repository)
            {
                this._repository = repository;
            }

            public async Task<Response<int>> Handle(DeleteBankAccountByIdCommand command, CancellationToken cancellationToken)
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
