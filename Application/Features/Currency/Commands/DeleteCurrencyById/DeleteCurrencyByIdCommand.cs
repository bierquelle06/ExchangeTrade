using Application.Exceptions;
using Application.Interfaces.Repositories;
using Application.Wrappers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Currency.Commands.DeleteCurrencyById
{
    /// <summary>
    /// DeleteCurrencyByIdCommand
    /// </summary>
    public class DeleteCurrencyByIdCommand : IRequest<Response<int>>
    {
        public int Id { get; set; }

        /// <summary>
        /// DeleteCurrencyByIdCommand -> Handler
        /// </summary>
        public class DeleteCurrencyByIdCommandHandler : IRequestHandler<DeleteCurrencyByIdCommand, Response<int>>
        {
            private readonly ICurrencyRepositoryAsync _repository;

            public DeleteCurrencyByIdCommandHandler(ICurrencyRepositoryAsync repository)
            {
                this._repository = repository;
            }

            public async Task<Response<int>> Handle(DeleteCurrencyByIdCommand command, CancellationToken cancellationToken)
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
