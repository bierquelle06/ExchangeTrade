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

namespace Application.Features.CurrencyActivity.Queries.GetAllCurrencyActivities
{
    /// <summary>
    /// GetAllCurrencyActivitysQuery
    /// </summary>
    public class GetAllCurrencyActivitiesQuery : IRequest<PagedResponse<IEnumerable<GetAllCurrencyActivitiesViewModel>>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }

    /// <summary>
    /// GetAllCurrencyActivitysQuery -> Handler
    /// </summary>
    public class GetAllCurrencyActivitiesQueryHandler : IRequestHandler<GetAllCurrencyActivitiesQuery, PagedResponse<IEnumerable<GetAllCurrencyActivitiesViewModel>>>
    {
        private readonly ICurrencyActivityRepositoryAsync _repository;
        private readonly IMapper _mapper;

        public GetAllCurrencyActivitiesQueryHandler(ICurrencyActivityRepositoryAsync CurrencyActivityRepository, IMapper mapper)
        {
            _repository = CurrencyActivityRepository;
            _mapper = mapper;
        }

        public async Task<PagedResponse<IEnumerable<GetAllCurrencyActivitiesViewModel>>> Handle(GetAllCurrencyActivitiesQuery request, CancellationToken cancellationToken)
        {
            var validFilter = _mapper.Map<GetAllCurrencyActivitiesParameter>(request);

            var entity = await _repository.GetPagedReponseAsync(x => x.IsDelete == false, 
                includeProperties: "", 
                pageNumber: validFilter.PageNumber, 
                pageSize: validFilter.PageSize, 
                hasPagination: true, 
                OrderBy: null);

            var viewModel = _mapper.Map<IEnumerable<GetAllCurrencyActivitiesViewModel>>(entity);

            return new PagedResponse<IEnumerable<GetAllCurrencyActivitiesViewModel>>(viewModel, validFilter.PageNumber, validFilter.PageSize);           
        }
    }
}
