using Application.Branch.Enums;
using Application.Interfaces.Repositories;
using Application.Wrappers;

using AutoMapper;

using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.BankBranch.Commands.CreateBankBranch
{
    /// <summary>
    /// CreateBranchCommand
    /// </summary>
    public partial class CreateBankBranchCommand : IRequest<Response<int>>
    {
        public string Name { get; }

        public int BankId { get; }

        public CreateBankBranchCommand(string name,
            int bankId)
        {
            this.Name = name;
            this.BankId = bankId;
        }

        public Domain.Entities.BankBranch.BankBranch CreateNewEntity()
        {
            return new Domain.Entities.BankBranch.BankBranch()
            {
                Name = this.Name,
                BankId = this.BankId
            };
        }
    }

    /// <summary>
    /// CreateBankBranchCommand -> Handler
    /// </summary>
    public class CreateBankBranchCommandHandler : IRequestHandler<CreateBankBranchCommand, Response<int>>
    {
        private readonly IBankBranchRepositoryAsync _bankBranchRepository;
        private readonly IMapper _mapper;

        public CreateBankBranchCommandHandler(IBankBranchRepositoryAsync bankBranchRepository, IMapper mapper)
        {
            _bankBranchRepository = bankBranchRepository;
            _mapper = mapper;
        }

        public async Task<Response<int>> Handle(CreateBankBranchCommand request, CancellationToken cancellationToken)
        {
            var entity = request.CreateNewEntity();

            await _bankBranchRepository.AddAsync(entity);

            return new Response<int>(entity.Id);
        }
    }
}
