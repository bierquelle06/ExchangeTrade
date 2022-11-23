using Application.Exceptions;
using Application.Interfaces.Repositories;
using Application.Wrappers;

using AutoMapper;

using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.BankAccount.Commands.CreateBankAccount
{
    /// <summary>
    /// CreateBankAccountCommand
    /// </summary>
    public partial class CreateBankAccountCommand : IRequest<Response<int>>
    {
        public string Name { get; }
        public string Code { get; }
        public string Number { get; }
        public string Iban { get; }

        public Nullable<DateTime> OpenDate { get;}
        public Nullable<decimal> TotalBalance { get; }
        public Nullable<decimal> TotalCreditBalance { get; }
        public Nullable<decimal> TotalCreditCardBalance { get; }
        public Nullable<decimal> BlockBalanceLimit { get; }
        public Nullable<decimal> BlockCreditLimit { get; }

        public int CustomerId { get; }

        public int BankAccountTypeId { get; }
        
        public int CurrencyId { get; }
        
        public int IntegratorId { get; }
        
        public int BankId { get; }

        public CreateBankAccountCommand(int customerId,
            int bankId,
            int integratorId,
            int currencyId,
            int bankAccountTypeId,
            string name,
            string code,
            string number,
            string iban,
            DateTime? openDate,
            decimal? totalBalance,
            decimal? totalCreditBalance,
            decimal? totalCreditCardBalance,
            decimal? blockBalanceLimit,
            decimal? blockCreditLimit)
        {
            this.BankId = bankId;
            this.IntegratorId = integratorId;
            this.CurrencyId = currencyId;
            this.BankAccountTypeId = bankAccountTypeId;
            this.CustomerId = customerId;

            this.Name = name;
            this.Code = code;
            this.Number = number;
            this.Iban = iban;

            this.OpenDate = openDate ?? DateTime.Now;

            this.TotalBalance = totalBalance;
            this.TotalCreditBalance = totalCreditBalance;
            this.TotalCreditCardBalance = totalCreditCardBalance;
            this.BlockBalanceLimit = blockBalanceLimit;
            this.BlockCreditLimit = blockCreditLimit;
        }

        public Domain.Entities.BankAccount.BankAccount CreateNewEntity()
        {
            return new Domain.Entities.BankAccount.BankAccount()
            {
                BankId = this.BankId,
                IntegratorId = this.IntegratorId,
                CurrencyId = this.CurrencyId,
                BankAccountTypeId = this.BankAccountTypeId,

                Number = this.Number,
                Iban = this.Iban,

                OpenDate = this.OpenDate,
                TotalBalance = this.TotalBalance,
                TotalCreditBalance = this.TotalCreditBalance,
                TotalCreditCardBalance = this.TotalCreditCardBalance,
                BlockBalanceLimit = this.BlockBalanceLimit,
                BlockCreditLimit = this.BlockCreditLimit
            };
        }
    }

    /// <summary>
    /// CreateBankAccountCommand -> Handler
    /// </summary>
    public class CreateBankAccountCommandHandler : IRequestHandler<CreateBankAccountCommand, Response<int>>
    {
        private readonly IBankAccountRepositoryAsync _repository;
        
        private readonly IBankRepositoryAsync _bankRepository;
        private readonly IIntegratorRepositoryAsync _integratorRepository;
        private readonly ICurrencyRepositoryAsync _currencyRepository;
        private readonly IBankAccountTypeRepositoryAsync _bankAccountTypeRepository;

        private readonly IMapper _mapper;

        public CreateBankAccountCommandHandler(IBankAccountRepositoryAsync repository,
            IBankRepositoryAsync bankRepository,
            IIntegratorRepositoryAsync integratorRepository,
            ICurrencyRepositoryAsync currencyRepository,
            IBankAccountTypeRepositoryAsync bankAccountTypeRepository,
            IMapper mapper)
        {
            this._repository = repository;

            this._bankRepository = bankRepository;
            this._integratorRepository = integratorRepository;
            this._currencyRepository = currencyRepository;
            this._bankAccountTypeRepository = bankAccountTypeRepository;

            this._mapper = mapper;
        }

        public async Task<Response<int>> Handle(CreateBankAccountCommand request, CancellationToken cancellationToken)
        {
            var bankEntity = await this._bankRepository.GetByIdAsync(request.BankId);
            if (bankEntity == null)
            {
                throw new ApiException(BusinessExceptions.BankError + " " + BusinessExceptions.NotFound);
            }

            if (bankEntity.IsDelete.HasValue && bankEntity.IsDelete.Value)
            {
                throw new ApiException(BusinessExceptions.BankError + " " + BusinessExceptions.DeletedRecord);
            }

            var integratorEntity = await this._integratorRepository.GetByIdAsync(request.IntegratorId);
            if (integratorEntity == null)
            {
                throw new ApiException(BusinessExceptions.IntegratorError + " " + BusinessExceptions.NotFound);
            }

            if (integratorEntity.IsDelete.HasValue && integratorEntity.IsDelete.Value)
            {
                throw new ApiException(BusinessExceptions.IntegratorError + " " + BusinessExceptions.DeletedRecord);
            }

            var currencyEntity = await this._currencyRepository.GetByIdAsync(request.CurrencyId);
            if (currencyEntity == null)
            {
                throw new ApiException(BusinessExceptions.CurrencyError + " " + BusinessExceptions.NotFound);
            }

            if (currencyEntity.IsDelete.HasValue && currencyEntity.IsDelete.Value)
            {
                throw new ApiException(BusinessExceptions.CurrencyError + " " + BusinessExceptions.DeletedRecord);
            }

            var bankAccountTypeEntity = await this._bankAccountTypeRepository.GetByIdAsync(request.BankAccountTypeId);
            if (bankAccountTypeEntity == null)
            {
                throw new ApiException(BusinessExceptions.BankAccountTypeError + " " + BusinessExceptions.NotFound);
            }

            if (bankAccountTypeEntity.IsDelete.HasValue && bankAccountTypeEntity.IsDelete.Value)
            {
                throw new ApiException(BusinessExceptions.BankAccountTypeError + " " + BusinessExceptions.DeletedRecord);
            }

            var entity = _mapper.Map<Domain.Entities.BankAccount.BankAccount>(request);

            await _repository.AddAsync(entity);

            return new Response<int>(entity.Id);
        }
    }
}
