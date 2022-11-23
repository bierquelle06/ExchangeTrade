using Application.Exceptions;
using Application.Interfaces.Repositories;
using Application.Wrappers;

using MediatR;

using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.CurrencyActivity.Commands.DeleteCurrencyActivityById
{
    /// <summary>
    /// DeleteCurrencyActivityByIdCommand
    /// </summary>
    public class DeleteCurrencyActivityByIdCommand : IRequest<Response<int>>
    {
        public int Id { get; set; }

        /// <summary>
        /// DeleteCurrencyActivityByIdCommand -> Handler
        /// </summary>
        public class DeleteCurrencyActivityByIdCommandHandler : IRequestHandler<DeleteCurrencyActivityByIdCommand, Response<int>>
        {
            private readonly ICurrencyActivityRepositoryAsync _repository;

            public DeleteCurrencyActivityByIdCommandHandler(ICurrencyActivityRepositoryAsync repository)
            {
                this._repository = repository;
            }

            public async Task<Response<int>> Handle(DeleteCurrencyActivityByIdCommand command, CancellationToken cancellationToken)
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
