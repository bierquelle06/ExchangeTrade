using Application.Interfaces.Repositories;
using Application.Wrappers;

using AutoMapper;

using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Integrator.Commands.CreateIntegrator
{
    /// <summary>
    /// CreateIntegratorCommand
    /// </summary>
    public partial class CreateIntegratorCommand : IRequest<Response<int>>
    {
        public string Name { get; }

        public string Code { get; }

        public string Host { get; }

        public string Port { get; }

        public string Url { get; }

        public string UserName { get; }

        public string Password { get; }

        public CreateIntegratorCommand(string name,
           string code,
           string host,
           string port,
           string url,
           string username,
           string password)
        {
            this.Name = name;
            this.Code = code;
            this.Host = host;
            this.Port = port;
            this.Url = url;
            this.UserName = username;
            this.Password = password;
        }

        public Domain.Entities.Integrator.Integrator CreateNewEntity()
        {
            return new Domain.Entities.Integrator.Integrator()
            {
                Name = this.Name,
                Code = this.Code,
                Host = this.Host,
                Port = this.Port,
                Url = this.Url,
                UserName = this.UserName,
                Password = this.Password,
            };
        }
    }

    /// <summary>
    /// CreateIntegratorCommand -> Handler
    /// </summary>
    public class CreateIntegratorCommandHandler : IRequestHandler<CreateIntegratorCommand, Response<int>>
    {
        private readonly IIntegratorRepositoryAsync _integratorRepository;
        private readonly IMapper _mapper;

        public CreateIntegratorCommandHandler(IIntegratorRepositoryAsync integratorRepository, IMapper mapper)
        {
            _integratorRepository = integratorRepository;
            _mapper = mapper;
        }

        public async Task<Response<int>> Handle(CreateIntegratorCommand request, CancellationToken cancellationToken)
        {
            var entity = request.CreateNewEntity();

            await _integratorRepository.AddAsync(entity);

            return new Response<int>(entity.Id);
        }
    }
}
