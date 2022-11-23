using Application.Exceptions;
using Application.Interfaces.Repositories;
using Application.Wrappers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Integrator.Commands.UpdateIntegrator
{
    public class UpdateIntegratorCommand : IRequest<Response<int>>
    {
        public int Id { get; }

        public string Name { get; set; }

        public string Code { get; set; }

        public string Host { get; set; }

        public string Port { get; set; }

        public string Url { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }

        public UpdateIntegratorCommand(int id)
        {
            this.Id = id;
        }
    }

    public class UpdateIntegratorCommandHandler : IRequestHandler<UpdateIntegratorCommand, Response<int>>
    {
        private readonly IIntegratorRepositoryAsync _integratorRepository;

        public UpdateIntegratorCommandHandler(IIntegratorRepositoryAsync integratorRepository)
        {
            _integratorRepository = integratorRepository;
        }

        public async Task<Response<int>> Handle(UpdateIntegratorCommand command, CancellationToken cancellationToken)
        {
            var entity = await _integratorRepository.GetByIdAsync(command.Id);

            if (entity == null)
            {
                throw new ApiException(BusinessExceptions.NotFound);
            }
            else if (entity.IsDelete.HasValue && entity.IsDelete.Value)
            {
                throw new ApiException(BusinessExceptions.DeletedRecord);
            }
            else
            {
                entity.Name = command.Name;
                entity.Code = command.Code;
                entity.Host = command.Host;
                entity.Port = command.Port;
                entity.Url = command.Url;
                entity.UserName = command.UserName;
                entity.Password = command.Password;

                await _integratorRepository.UpdateAsync(entity);

                return new Response<int>(entity.Id);
            }
        }
    }
}
