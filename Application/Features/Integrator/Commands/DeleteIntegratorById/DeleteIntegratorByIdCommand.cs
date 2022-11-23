using Application.Exceptions;
using Application.Interfaces.Repositories;
using Application.Wrappers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Integrator.Commands.DeleteIntegratorById
{
    /// <summary>
    /// DeleteIntegratorByIdCommand
    /// </summary>
    public class DeleteIntegratorByIdCommand : IRequest<Response<int>>
    {
        public int Id { get; set; }

        /// <summary>
        /// DeleteIntegratorByIdCommand -> Handler
        /// </summary>
        public class DeleteIntegratorByIdCommandHandler : IRequestHandler<DeleteIntegratorByIdCommand, Response<int>>
        {
            private readonly IIntegratorRepositoryAsync _integratorRepository;

            public DeleteIntegratorByIdCommandHandler(IIntegratorRepositoryAsync integratorRepository)
            {
                _integratorRepository = integratorRepository;
            }

            public async Task<Response<int>> Handle(DeleteIntegratorByIdCommand command, CancellationToken cancellationToken)
            {
                var entity = await _integratorRepository.GetByIdAsync(command.Id);

                if (entity == null) 
                    throw new ApiException(BusinessExceptions.NotFound);
                
                await _integratorRepository.DeleteSoftAsync(entity.Id);
                
                return new Response<int>(entity.Id);
            }
        }
    }
}
