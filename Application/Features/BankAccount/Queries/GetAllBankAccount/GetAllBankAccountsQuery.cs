using Application.Features.BankAccount.Queries.GetAllBankAccounts;
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

namespace Application.Features.BankAccount.Queries.GetAllBankAccounts
{
    /// <summary>
    /// GetAllBankAccountsQuery
    /// </summary>
    public class GetAllBankAccountsQuery : IRequest<PagedResponse<IEnumerable<GetAllBankAccountsViewModel>>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }

    /// <summary>
    /// GetAllBankAccountsQuery -> Handler
    /// </summary>
    public class GetAllBankAccountsQueryHandler : IRequestHandler<GetAllBankAccountsQuery, PagedResponse<IEnumerable<GetAllBankAccountsViewModel>>>
    {
        private readonly IBankAccountRepositoryAsync _repository;
        private readonly IMapper _mapper;

        public GetAllBankAccountsQueryHandler(IBankAccountRepositoryAsync BankAccountRepository, IMapper mapper)
        {
            _repository = BankAccountRepository;
            _mapper = mapper;
        }

        public async Task<PagedResponse<IEnumerable<GetAllBankAccountsViewModel>>> Handle(GetAllBankAccountsQuery request, CancellationToken cancellationToken)
        {
            var validFilter = _mapper.Map<GetAllBankAccountsParameter>(request);

            var entity = await _repository.GetPagedReponseAsync(x => x.IsDelete == false, 
                includeProperties: "", 
                pageNumber: validFilter.PageNumber, 
                pageSize: validFilter.PageSize, 
                hasPagination: true, 
                OrderBy: null);

            var viewModel = _mapper.Map<IEnumerable<GetAllBankAccountsViewModel>>(entity);

            return new PagedResponse<IEnumerable<GetAllBankAccountsViewModel>>(viewModel, validFilter.PageNumber, validFilter.PageSize);           
        }
    }
}
