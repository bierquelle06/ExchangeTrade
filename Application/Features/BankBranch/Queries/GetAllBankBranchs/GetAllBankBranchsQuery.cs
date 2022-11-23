using Application.Features.BankBranch.Queries.GetAllBankBranchs;
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

namespace Application.Features.BankBranch.Queries.GetAllBankBranchs
{
    /// <summary>
    /// GetAllBankBranchsQuery
    /// </summary>
    public class GetAllBankBranchsQuery : IRequest<PagedResponse<IEnumerable<GetAllBankBranchsViewModel>>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }

    /// <summary>
    /// GetAllBankBranchsQuery -> Handler
    /// </summary>
    public class GetAllBranchsQueryHandler : IRequestHandler<GetAllBankBranchsQuery, PagedResponse<IEnumerable<GetAllBankBranchsViewModel>>>
    {
        private readonly IBankBranchRepositoryAsync _branchRepository;
        private readonly IMapper _mapper;

        public GetAllBranchsQueryHandler(IBankBranchRepositoryAsync BranchRepository, IMapper mapper)
        {
            _branchRepository = BranchRepository;
            _mapper = mapper;
        }

        public async Task<PagedResponse<IEnumerable<GetAllBankBranchsViewModel>>> Handle(GetAllBankBranchsQuery request, CancellationToken cancellationToken)
        {
            var validFilter = _mapper.Map<GetAllBankBranchsParameter>(request);

            var branch = await _branchRepository.GetPagedReponseAsync(validFilter.PageNumber, validFilter.PageSize, x => x.IsDelete == false);

            var bankBranchViewModel = _mapper.Map<IEnumerable<GetAllBankBranchsViewModel>>(branch);

            return new PagedResponse<IEnumerable<GetAllBankBranchsViewModel>>(bankBranchViewModel, validFilter.PageNumber, validFilter.PageSize);           
        }
    }
}
