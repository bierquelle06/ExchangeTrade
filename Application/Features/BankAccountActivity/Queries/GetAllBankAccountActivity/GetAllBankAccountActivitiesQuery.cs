using Application.Features.BankAccountActivity.Queries.GetAllBankAccountActivities;
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

namespace Application.Features.BankAccountActivity.Queries.GetAllBankAccountActivities
{
    /// <summary>
    /// GetAllBankAccountActivitysQuery
    /// </summary>
    public class GetAllBankAccountActivitiesQuery : IRequest<PagedResponse<IEnumerable<GetAllBankAccountActivitiesViewModel>>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }

    /// <summary>
    /// GetAllBankAccountActivitysQuery -> Handler
    /// </summary>
    public class GetAllBankAccountActivitysQueryHandler : IRequestHandler<GetAllBankAccountActivitiesQuery, PagedResponse<IEnumerable<GetAllBankAccountActivitiesViewModel>>>
    {
        private readonly IBankAccountActivityRepositoryAsync _repository;
        private readonly IMapper _mapper;

        public GetAllBankAccountActivitysQueryHandler(IBankAccountActivityRepositoryAsync BankAccountActivityRepository, IMapper mapper)
        {
            _repository = BankAccountActivityRepository;
            _mapper = mapper;
        }

        public async Task<PagedResponse<IEnumerable<GetAllBankAccountActivitiesViewModel>>> Handle(GetAllBankAccountActivitiesQuery request, CancellationToken cancellationToken)
        {
            var validFilter = _mapper.Map<GetAllBankAccountActivitiesParameter>(request);

            var entity = await _repository.GetPagedReponseAsync(x => x.IsDelete == false, 
                includeProperties: "", 
                pageNumber: validFilter.PageNumber, 
                pageSize: validFilter.PageSize, 
                hasPagination: true, 
                OrderBy: null);

            var viewModel = _mapper.Map<IEnumerable<GetAllBankAccountActivitiesViewModel>>(entity);

            return new PagedResponse<IEnumerable<GetAllBankAccountActivitiesViewModel>>(viewModel, validFilter.PageNumber, validFilter.PageSize);           
        }
    }
}
