using Application.Exceptions;
using Application.Interfaces.Repositories;
using Application.Wrappers;

using AutoMapper;

using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Bank.Commands.CreateBank
{
    /// <summary>
    /// CreateBankCommand
    /// </summary>
    public partial class CreateBankCommand : IRequest<Response<int>>
    {
        public string Name { get; }

        public CreateBankCommand(string name)
        {
            this.Name = name;
        }

        public Domain.Entities.Bank.Bank CreateNewEntity()
        {
            return new Domain.Entities.Bank.Bank()
            {
                Name = this.Name
            };
        }
    }

    /// <summary>
    /// CreateBankCommand -> Handler
    /// </summary>
    public class CreateBankCommandHandler : IRequestHandler<CreateBankCommand, Response<int>>
    {
        private readonly IBankRepositoryAsync _repository;
        private readonly IBankBranchRepositoryAsync _branchRepository;

        private readonly IMapper _mapper;

        public CreateBankCommandHandler(IBankRepositoryAsync repository, IBankBranchRepositoryAsync branchRepository, IMapper mapper)
        {
            this._repository = repository;
            this._branchRepository = branchRepository;

            this._mapper = mapper;
        }

        public async Task<Response<int>> Handle(CreateBankCommand request, CancellationToken cancellationToken)
        {
            var entity = _mapper.Map<Domain.Entities.Bank.Bank>(request);

            await _repository.AddAsync(entity);

            return new Response<int>(entity.Id);
        }
    }
}
