using Application.BankActivity.Enums;
using Application.Exceptions;
using Application.Interfaces.Repositories;
using Application.Wrappers;

using AutoMapper;

using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.BankAccountActivity.Commands.CreateBankAccountActivity
{
    /// <summary>
    /// CreateBankAccountActivityCommand
    /// </summary>
    public partial class CreateBankAccountActivityCommand : IRequest<Response<int>>
    {
        public int BankAccountId { get; }

        public Nullable<decimal> Quantity { get; }
        public string CurrencyCode { get; }
        public string Description { get; }
        public string Source { get; }
        public int ProcessID { get; }
        public string ProcessName { get; }
        public Nullable<DateTime> ProcessDate { get; }
        public Nullable<decimal> Balance { get; }
        public string ReceiptNo { get; }
        public string Note { get; }

        public CreateBankAccountActivityCommand(int bankAccountId,
            string currencyCode,
            string description,
            string source,
            int processID,
            string processName,
            DateTime? processDate,
            decimal? quantity,
            decimal? balance,
            string receiptNo,
            string note)
        {
            this.BankAccountId = bankAccountId;

            this.Quantity = quantity;
            this.CurrencyCode = currencyCode;
            this.Description = description;
            this.Source = source;
            this.ProcessID = processID;
            this.ProcessName = processName;
            this.ProcessDate = processDate;

            this.Balance = balance;
            this.ReceiptNo = receiptNo;
            this.Note = note;
        }

        public Domain.Entities.BankAccountActivity.BankAccountActivity CreateNewEntity()
        {
            return new Domain.Entities.BankAccountActivity.BankAccountActivity()
            {
                BankAccountId = this.BankAccountId,
                Quantity = this.Quantity,
                CurrencyCode = this.CurrencyCode,
                Description = this.Description,
                Source = this.Source,
                ProcessID = this.ProcessID,
                ProcessName = this.ProcessName,
                ProcessDate = this.ProcessDate,
                ReceiptNo = this.ReceiptNo,
                Balance = this.Balance,
                Note = this.Note
            };
        }
    }

    /// <summary>
    /// CreateBankAccountActivityCommand -> Handler
    /// </summary>
    public class CreateBankAccountActivityCommandHandler : IRequestHandler<CreateBankAccountActivityCommand, Response<int>>
    {
        private readonly IBankAccountActivityRepositoryAsync _repository;

        private readonly IBankAccountRepositoryAsync _bankAccountRepository;
        private readonly ICurrencyRepositoryAsync _currencyRepository;
        private readonly ICurrencyActivityRepositoryAsync _currencyActivityRepository;

        private readonly IMapper _mapper;

        public CreateBankAccountActivityCommandHandler(IBankAccountActivityRepositoryAsync repository,
            IBankAccountRepositoryAsync bankAccountRepository,
            ICurrencyRepositoryAsync currencyRepository,
            ICurrencyActivityRepositoryAsync currencyActivityRepository,
            IMapper mapper)
        {
            this._repository = repository;

            this._bankAccountRepository = bankAccountRepository;
            this._currencyRepository = currencyRepository;
            this._currencyActivityRepository = currencyActivityRepository;

            this._mapper = mapper;
        }

        public async Task<Response<int>> Handle(CreateBankAccountActivityCommand request, CancellationToken cancellationToken)
        {
            var bankAccountEntity = await this._bankAccountRepository.GetByIdAsync(request.BankAccountId);
            if (bankAccountEntity == null)
            {
                throw new ApiException(BusinessExceptions.BankError + " " + BusinessExceptions.NotFound);
            }

            if (bankAccountEntity.IsDelete.HasValue && bankAccountEntity.IsDelete.Value)
            {
                throw new ApiException(BusinessExceptions.BankAccountError + " " + BusinessExceptions.DeletedRecord);
            }

            var currencyEntity = await this._currencyRepository.Find(x => x.Code.Equals(request.CurrencyCode) && x.IsDelete == false).FirstOrDefaultAsync();
            if (currencyEntity == null)
            {
                throw new ApiException(BusinessExceptions.CurrencyError + " " + BusinessExceptions.NotFound);
            }

            if (request.ProcessID == (int)CurrencyIdentificationType.Unknown)
            {
                throw new ApiException(BusinessExceptions.ProcessError + " " + BusinessExceptions.UnknownType + "(Buy => 1 / Sell => 2)");
            }

            var count = await this._repository.Find(x => x.ProcessDate >= DateTime.Now.AddHours(-1) && x.BankAccountId == request.BankAccountId).CountAsync();
            if (count > 10)
            {
                throw new ApiException("Sorry, maximum 10 currency exchange trades per hour!");
            }

            //Calculation Currency
            var currencyBaseEntity = await this._currencyRepository.Find(x => x.Id == bankAccountEntity.CurrencyId && x.IsDelete == false).FirstOrDefaultAsync();

            var currencyActivity = await _currencyActivityRepository.Find(x => x.Date == DateTime.Now.ToString("yyyy-MM-dd") && x.SymbolBase == currencyBaseEntity.Code && x.Symbol == request.CurrencyCode).FirstOrDefaultAsync();
           
            var entity = _mapper.Map<Domain.Entities.BankAccountActivity.BankAccountActivity>(request);

            //Calculate Balance
            if (currencyBaseEntity.Code == currencyActivity.SymbolBase)
            {
                if (request.ProcessID == (int)CurrencyIdentificationType.Buy)
                    entity.Balance += request.Quantity * 1;

                if (request.ProcessID == (int)CurrencyIdentificationType.Sell)
                    entity.Balance -= request.Quantity * 1;
            }
            else
            {
                if (request.ProcessID == (int)CurrencyIdentificationType.Buy)
                {
                    var currencyActivityBuy = await _currencyActivityRepository.Find(x => x.Date == DateTime.Now.ToString("yyyy-MM-dd") 
                    && x.SymbolBase == request.CurrencyCode
                    && x.Symbol == currencyBaseEntity.Code).FirstOrDefaultAsync();

                    entity.Balance += request.Quantity * currencyActivityBuy.Rate;
                }

                if (request.ProcessID == (int)CurrencyIdentificationType.Sell)
                {
                    throw new ApiException("This is " + currencyBaseEntity.Name + " account! Sorry, you can't sell to " + currencyActivity.SymbolBase + " with this account.");
                }
            }

            await _repository.AddAsync(entity);

            return new Response<int>(entity.Id);
        }
    }
}
