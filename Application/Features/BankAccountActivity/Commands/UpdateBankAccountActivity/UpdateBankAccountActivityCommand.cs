using Application.BankActivity.Enums;
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

namespace Application.Features.BankAccountActivity.Commands.UpdateBankAccountActivity
{
    public class UpdateBankAccountActivityCommand : IRequest<Response<int>>
    {
        public int Id { get; }

        public int BankAccountId { get; set; }

        public Nullable<decimal> Quantity { get; set; }
        public string CurrencyCode { get; set; }
        public string Description { get; set; }
        public string Source { get; set; }
        public int ProcessID { get; set; }
        public string ProcessName { get; set; }
        public Nullable<DateTime> ProcessDate { get; set; }
        public Nullable<decimal> Balance { get; set; }
        public string ReceiptNo { get; set; }
        public string Note { get; set; }

        public UpdateBankAccountActivityCommand(int id)
        {
            this.Id = id;
        }
    }

    public class UpdateBankAccountActivityCommandHandler : IRequestHandler<UpdateBankAccountActivityCommand, Response<int>>
    {
        private readonly IBankAccountActivityRepositoryAsync _repository;

        private readonly IBankAccountRepositoryAsync _bankAccountRepository;
        private readonly ICurrencyRepositoryAsync _currencyRepository;

        private readonly IMapper _mapper;

        public UpdateBankAccountActivityCommandHandler(IBankAccountActivityRepositoryAsync repository,
            IBankAccountRepositoryAsync bankAccountRepository,
            ICurrencyRepositoryAsync currencyRepository,
            IMapper mapper)
        {
            this._repository = repository;

            this._bankAccountRepository = bankAccountRepository;
            this._currencyRepository = currencyRepository;

            this._mapper = mapper;
        }

        public async Task<Response<int>> Handle(UpdateBankAccountActivityCommand command, CancellationToken cancellationToken)
        {
            var bankAccountEntity = await this._bankAccountRepository.GetByIdAsync(command.BankAccountId);
            if (bankAccountEntity == null)
            {
                throw new ApiException(BusinessExceptions.BankError + " " + BusinessExceptions.NotFound);
            }

            if (bankAccountEntity.IsDelete.HasValue && bankAccountEntity.IsDelete.Value)
            {
                throw new ApiException(BusinessExceptions.BankAccountError + " " + BusinessExceptions.DeletedRecord);
            }

            var currencyEntity = await this._currencyRepository.Find(x => x.Code.Equals(command.CurrencyCode) && x.IsDelete == false).FirstOrDefaultAsync();
            if (currencyEntity == null)
            {
                throw new ApiException(BusinessExceptions.CurrencyError + " " + BusinessExceptions.NotFound);
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
                entity.BankAccountId = command.BankAccountId;

                entity.Quantity = command.Quantity ?? 0;
                entity.CurrencyCode = command.CurrencyCode;
                entity.Description = command.Description;
                entity.Source = command.Source;
                
                entity.ProcessID = command.ProcessID;
                entity.ProcessName = command.ProcessName;
                entity.ProcessDate = command.ProcessDate ?? DateTime.Now;
                
                entity.Balance = command.Balance ?? 0;

                entity.ReceiptNo = command.ReceiptNo;
                entity.Note = command.Note;

                await this._repository.UpdateAsync(entity);

                return new Response<int>(entity.Id);
            }
        }
    }
}
