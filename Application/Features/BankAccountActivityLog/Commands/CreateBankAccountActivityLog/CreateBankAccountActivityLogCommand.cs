using Application.Exceptions;
using Application.Interfaces.Repositories;
using Application.Wrappers;

using AutoMapper;

using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.BankAccount.Commands.CreateBankAccountActivityLog
{
    /// <summary>
    /// CreateBankAccountActivityLogCommand
    /// </summary>
    public partial class CreateBankAccountActivityLogCommand : IRequest<Response<int>>
    {
        public int BankAccountId { get; }

        public int ProcessId { get; }

        public CreateBankAccountActivityLogCommand(int bankAccountId, int processId)
        {
            this.BankAccountId = bankAccountId;
            this.ProcessId = processId;
        }

        public Domain.Entities.BankAccountActivityLog.BankAccountActivityLog CreateNewEntity()
        {
            return new Domain.Entities.BankAccountActivityLog.BankAccountActivityLog()
            {
                BankAccountId = this.BankAccountId,
                ProcessId = this.ProcessId
            };
        }
    }

    /// <summary>
    /// CreateBankAccountActivityLogCommand -> Handler
    /// </summary>
    public class CreateBankAccountActivityLogCommandHandler : IRequestHandler<CreateBankAccountActivityLogCommand, Response<int>>
    {
        private readonly IBankAccountActivityLogRepositoryAsync _repository;

        private readonly IMapper _mapper;

        public CreateBankAccountActivityLogCommandHandler(IBankAccountActivityLogRepositoryAsync repository, IMapper mapper)
        {
            this._repository = repository;

            this._mapper = mapper;
        }

        public async Task<Response<int>> Handle(CreateBankAccountActivityLogCommand request, CancellationToken cancellationToken)
        {
            var entity = _mapper.Map<Domain.Entities.BankAccountActivityLog.BankAccountActivityLog>(request);

            await _repository.AddAsync(entity);

            return new Response<int>(entity.Id);
        }
    }
}
