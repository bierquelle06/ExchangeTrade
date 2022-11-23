using Application.Exceptions;
using Application.Interfaces.Repositories;
using Application.Wrappers;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.BankAccount.Commands.UpdateBankAccount
{
    public class UpdateBankAccountCommand : IRequest<Response<int>>
    {
        public int Id { get; }

        public string Name { get; set; }
        public string Code { get; set; }
        public string Number { get; set; }
        public string Iban { get; set; }

        public Nullable<DateTime> OpenDate { get; set; }
        public Nullable<decimal> TotalBalance { get; set; }
        public Nullable<decimal> TotalCreditBalance { get; set; }
        public Nullable<decimal> TotalCreditCardBalance { get; set; }
        public Nullable<decimal> BlockBalanceLimit { get; set; }
        public Nullable<decimal> BlockCreditLimit { get; set; }

        public int CustomerId { get; set; }

        public int BankAccountTypeId { get; set; }

        public int CurrencyId { get; set; }

        public int IntegratorId { get; set; }

        public int BankId { get; set; }

        public UpdateBankAccountCommand(int id)
        {
            this.Id = id;
        }
    }

    public class UpdateBankAccountCommandHandler : IRequestHandler<UpdateBankAccountCommand, Response<int>>
    {
        private readonly IBankAccountRepositoryAsync _repository;

        private readonly IBankRepositoryAsync _bankRepository;
        private readonly IIntegratorRepositoryAsync _integratorRepository;
        private readonly ICurrencyRepositoryAsync _currencyRepository;
        private readonly IBankAccountTypeRepositoryAsync _bankAccountTypeRepository;

        private readonly IMapper _mapper;

        public UpdateBankAccountCommandHandler(IBankAccountRepositoryAsync repository, 
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

        public async Task<Response<int>> Handle(UpdateBankAccountCommand command, CancellationToken cancellationToken)
        {
            var bankEntity = await this._bankRepository.GetByIdAsync(command.BankId);
            if (bankEntity == null)
            {
                throw new ApiException(BusinessExceptions.BankError + " " + BusinessExceptions.NotFound);
            }

            if (bankEntity.IsDelete.HasValue && bankEntity.IsDelete.Value)
            {
                throw new ApiException(BusinessExceptions.BankError + " " + BusinessExceptions.DeletedRecord);
            }

            var integratorEntity = await this._integratorRepository.GetByIdAsync(command.IntegratorId);
            if (integratorEntity == null)
            {
                throw new ApiException(BusinessExceptions.IntegratorError + " " + BusinessExceptions.NotFound);
            }

            if (integratorEntity.IsDelete.HasValue && integratorEntity.IsDelete.Value)
            {
                throw new ApiException(BusinessExceptions.IntegratorError + " " + BusinessExceptions.DeletedRecord);
            }

            var currencyEntity = await this._currencyRepository.GetByIdAsync(command.CurrencyId);
            if (currencyEntity == null)
            {
                throw new ApiException(BusinessExceptions.CurrencyError + " " + BusinessExceptions.NotFound);
            }

            if (currencyEntity.IsDelete.HasValue && currencyEntity.IsDelete.Value)
            {
                throw new ApiException(BusinessExceptions.CurrencyError + " " + BusinessExceptions.DeletedRecord);
            }

            var bankAccountTypeEntity = await this._bankAccountTypeRepository.GetByIdAsync(command.BankAccountTypeId);
            if (bankAccountTypeEntity == null)
            {
                throw new ApiException(BusinessExceptions.BankAccountTypeError + " " + BusinessExceptions.NotFound);
            }

            if (bankAccountTypeEntity.IsDelete.HasValue && bankAccountTypeEntity.IsDelete.Value)
            {
                throw new ApiException(BusinessExceptions.BankAccountTypeError + " " + BusinessExceptions.DeletedRecord);
            }

            var entity = await this._repository.GetByIdAsync(command.Id);

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
                entity.CustomerId = command.CustomerId;
                entity.BankAccountTypeId = command.BankAccountTypeId;
                entity.CurrencyId = command.CurrencyId;
                entity.IntegratorId = command.IntegratorId;
                entity.BankId = command.BankId;

                entity.OpenDate = command.OpenDate;

                entity.Name = command.Name;
                entity.Code = command.Code;

                entity.Number = command.Number;
                entity.Iban = command.Iban;
                
                entity.TotalBalance = command.TotalBalance ?? 0;
                entity.TotalCreditBalance = command.TotalCreditBalance ?? 0;
                entity.TotalCreditCardBalance = command.TotalCreditCardBalance ?? 0;
                entity.BlockBalanceLimit = command.BlockBalanceLimit ?? 0;
                entity.BlockCreditLimit = command.BlockCreditLimit ?? 0;

                await this._repository.UpdateAsync(entity);

                return new Response<int>(entity.Id);
            }
        }
    }
}
