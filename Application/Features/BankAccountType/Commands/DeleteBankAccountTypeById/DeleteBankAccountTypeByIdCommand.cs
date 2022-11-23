using Application.Exceptions;
using Application.Interfaces.Repositories;
using Application.Wrappers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.BankAccountType.Commands.DeleteBankAccountTypeById
{
    /// <summary>
    /// DeleteBankAccountTypeByIdCommand
    /// </summary>
    public class DeleteBankAccountTypeByIdCommand : IRequest<Response<int>>
    {
        public int Id { get; set; }

        /// <summary>
        /// DeleteBankAccountTypeByIdCommand -> Handler
        /// </summary>
        public class DeleteBankAccountTypeByIdCommandHandler : IRequestHandler<DeleteBankAccountTypeByIdCommand, Response<int>>
        {
            private readonly IBankAccountTypeRepositoryAsync _repository;

            public DeleteBankAccountTypeByIdCommandHandler(IBankAccountTypeRepositoryAsync repository)
            {
                this._repository = repository;
            }

            public async Task<Response<int>> Handle(DeleteBankAccountTypeByIdCommand command, CancellationToken cancellationToken)
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
