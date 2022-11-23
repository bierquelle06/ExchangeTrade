using Application.Exceptions;
using Application.Interfaces.Repositories;
using Application.Wrappers;

using AutoMapper;

using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.CurrencyActivity.Commands.CreateCurrencyActivity
{
    /// <summary>
    /// CreateBankAccountActivityCommand
    /// </summary>
    public partial class CreateCurrencyActivityCommand : IRequest<Response<int>>
    {
        public int CurrencyId { get; }
        public string Symbol { get; }
        public decimal Rate { get; }

        public CreateCurrencyActivityCommand(int currencyId, string symbol, decimal? rate)
        {
            this.CurrencyId = currencyId;
            this.Symbol = symbol;
            this.Rate = rate ?? 0;
        }

        public Domain.Entities.CurrencyActivity.CurrencyActivity CreateNewEntity()
        {
            return new Domain.Entities.CurrencyActivity.CurrencyActivity()
            {
                CurrencyId = this.CurrencyId,
                Symbol = this.Symbol,
                Rate = this.Rate
        };
        }
    }

    /// <summary>
    /// CreateCurrencyActivityCommand -> Handler
    /// </summary>
    public class CreateCurrencyActivityCommandHandler : IRequestHandler<CreateCurrencyActivityCommand, Response<int>>
    {
        private readonly ICurrencyActivityRepositoryAsync _repository;

        private readonly ICurrencyRepositoryAsync _currencyRepository;

        private readonly IMapper _mapper;

        public CreateCurrencyActivityCommandHandler(ICurrencyActivityRepositoryAsync repository,
            ICurrencyRepositoryAsync currencyRepository,
            IMapper mapper)
        {
            this._repository = repository;
            this._currencyRepository = currencyRepository;

            this._mapper = mapper;
        }

        public async Task<Response<int>> Handle(CreateCurrencyActivityCommand request, CancellationToken cancellationToken)
        {
            var currencyEntity = await this._currencyRepository.GetByIdAsync(request.CurrencyId);
            if (currencyEntity == null)
            {
                throw new ApiException(BusinessExceptions.CurrencyError + " " + BusinessExceptions.NotFound);
            }

            var entity = _mapper.Map<Domain.Entities.CurrencyActivity.CurrencyActivity>(request);

            await _repository.AddAsync(entity);

            return new Response<int>(entity.Id);
        }
    }
}
