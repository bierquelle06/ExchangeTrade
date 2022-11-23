using Application.Features.Currency.Queries.GetAllCurrency;
using Application.Filters;
using Application.Interfaces.Repositories;
using Application.Wrappers;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Currency.Queries.GetAllCurrency
{
    /// <summary>
    /// GetAllCurrencyUnit
    /// </summary>
    public class GetAllCurrencyQuery : IRequest<PagedResponse<IEnumerable<GetAllCurrencyViewModel>>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }

    /// <summary>
    /// GetAllCurrencysQuery -> Handler
    /// </summary>
    public class GetAllCurrencysQueryHandler : IRequestHandler<GetAllCurrencyQuery, PagedResponse<IEnumerable<GetAllCurrencyViewModel>>>
    {
        private readonly ICurrencyRepositoryAsync _repository;
        private readonly IMapper _mapper;

        public GetAllCurrencysQueryHandler(ICurrencyRepositoryAsync CurrencyRepository, IMapper mapper)
        {
            _repository = CurrencyRepository;
            _mapper = mapper;
        }

        public async Task<PagedResponse<IEnumerable<GetAllCurrencyViewModel>>> Handle(GetAllCurrencyQuery request, CancellationToken cancellationToken)
        {
            var validFilter = _mapper.Map<GetAllCurrencyParameter>(request);

            var currency = await _repository.GetPagedReponseAsync(validFilter.PageNumber, validFilter.PageSize, x => x.IsDelete == true);

            var currencyViewModel = _mapper.Map<IEnumerable<GetAllCurrencyViewModel>>(currency);

            return new PagedResponse<IEnumerable<GetAllCurrencyViewModel>>(currencyViewModel, validFilter.PageNumber, validFilter.PageSize);           
        }
    }
}
