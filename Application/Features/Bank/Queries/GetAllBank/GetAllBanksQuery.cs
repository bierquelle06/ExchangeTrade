using Application.Features.Bank.Queries.GetAllBanks;
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

namespace Application.Features.Bank.Queries.GetAllBanks
{
    /// <summary>
    /// GetAllBanksQuery
    /// </summary>
    public class GetAllBanksQuery : IRequest<PagedResponse<IEnumerable<GetAllBanksViewModel>>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }

    /// <summary>
    /// GetAllBanksQuery -> Handler
    /// </summary>
    public class GetAllBanksQueryHandler : IRequestHandler<GetAllBanksQuery, PagedResponse<IEnumerable<GetAllBanksViewModel>>>
    {
        private readonly IBankRepositoryAsync _repository;
        private readonly IMapper _mapper;

        public GetAllBanksQueryHandler(IBankRepositoryAsync BankRepository, IMapper mapper)
        {
            _repository = BankRepository;
            _mapper = mapper;
        }

        public async Task<PagedResponse<IEnumerable<GetAllBanksViewModel>>> Handle(GetAllBanksQuery request, CancellationToken cancellationToken)
        {
            var validFilter = _mapper.Map<GetAllBanksParameter>(request);

            var entity = await _repository.GetPagedReponseAsync(x => x.IsDelete == false, 
                includeProperties: "", 
                pageNumber: validFilter.PageNumber, 
                pageSize: validFilter.PageSize, 
                hasPagination: true, 
                OrderBy: null);

            var viewModel = _mapper.Map<IEnumerable<GetAllBanksViewModel>>(entity);

            return new PagedResponse<IEnumerable<GetAllBanksViewModel>>(viewModel, validFilter.PageNumber, validFilter.PageSize);           
        }
    }
}
