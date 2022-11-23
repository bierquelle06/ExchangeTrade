using Application.Exceptions;
using Application.Interfaces.Repositories;
using Application.Wrappers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Bank.Commands.DeleteBankById
{
    /// <summary>
    /// DeleteBankByIdCommand
    /// </summary>
    public class DeleteBankByIdCommand : IRequest<Response<int>>
    {
        public int Id { get; set; }

        /// <summary>
        /// DeleteBankByIdCommand -> Handler
        /// </summary>
        public class DeleteBankByIdCommandHandler : IRequestHandler<DeleteBankByIdCommand, Response<int>>
        {
            private readonly IBankRepositoryAsync _repository;

            public DeleteBankByIdCommandHandler(IBankRepositoryAsync repository)
            {
                this._repository = repository;
            }

            public async Task<Response<int>> Handle(DeleteBankByIdCommand command, CancellationToken cancellationToken)
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
