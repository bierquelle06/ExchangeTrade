using Application.Interfaces.Repositories;
using Application.Wrappers;

using AutoMapper;

using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.BankAccountType.Commands.CreateBankAccountType
{
    /// <summary>
    /// CreateBankAccountTypeCommand
    /// </summary>
    public partial class CreateBankAccountTypeCommand : IRequest<Response<int>>
    {
        public string Name { get; }

        public CreateBankAccountTypeCommand(string name)
        {
            this.Name = name;
        }

        public Domain.Entities.BankAccountType.BankAccountType CreateNewEntity()
        {
            return new Domain.Entities.BankAccountType.BankAccountType()
            {
                Name = this.Name
            };
        }
    }

    /// <summary>
    /// CreateBankAccountTypeCommand -> Handler
    /// </summary>
    public class CreateBankAccountTypeCommandHandler : IRequestHandler<CreateBankAccountTypeCommand, Response<int>>
    {
        private readonly IBankAccountTypeRepositoryAsync _BankAccountTypeRepository;
        private readonly IMapper _mapper;

        public CreateBankAccountTypeCommandHandler(IBankAccountTypeRepositoryAsync BankAccountTypeRepository, IMapper mapper)
        {
            _BankAccountTypeRepository = BankAccountTypeRepository;
            _mapper = mapper;
        }

        public async Task<Response<int>> Handle(CreateBankAccountTypeCommand request, CancellationToken cancellationToken)
        {
            var entity = _mapper.Map<Domain.Entities.BankAccountType.BankAccountType>(request);

            await _BankAccountTypeRepository.AddAsync(entity);

            return new Response<int>(entity.Id);
        }
    }
}
