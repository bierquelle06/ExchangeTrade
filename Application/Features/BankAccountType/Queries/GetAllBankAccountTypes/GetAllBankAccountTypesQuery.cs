using Application.Features.BankAccountType.Queries.GetAllBankAccountTypes;
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

namespace Application.Features.BankAccountType.Queries.GetAllBankAccountTypes
{
    /// <summary>
    /// GetAllBankAccountTypesQuery
    /// </summary>
    public class GetAllBankAccountTypesQuery : IRequest<PagedResponse<IEnumerable<GetAllBankAccountTypesViewModel>>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }

    /// <summary>
    /// GetAllBankAccountTypesQuery -> Handler
    /// </summary>
    public class GetAllBankAccountTypesQueryHandler : IRequestHandler<GetAllBankAccountTypesQuery, PagedResponse<IEnumerable<GetAllBankAccountTypesViewModel>>>
    {
        private readonly IBankAccountTypeRepositoryAsync _repository;
        private readonly IMapper _mapper;

        public GetAllBankAccountTypesQueryHandler(IBankAccountTypeRepositoryAsync BankAccountTypeRepository, IMapper mapper)
        {
            _repository = BankAccountTypeRepository;
            _mapper = mapper;
        }

        public async Task<PagedResponse<IEnumerable<GetAllBankAccountTypesViewModel>>> Handle(GetAllBankAccountTypesQuery request, CancellationToken cancellationToken)
        {
            var validFilter = _mapper.Map<GetAllBankAccountTypesParameter>(request);

            var BankAccountType = await _repository.GetPagedReponseAsync(validFilter.PageNumber, validFilter.PageSize, x => x.IsDelete == false);

            var BankAccountTypeViewModel = _mapper.Map<IEnumerable<GetAllBankAccountTypesViewModel>>(BankAccountType);

            return new PagedResponse<IEnumerable<GetAllBankAccountTypesViewModel>>(BankAccountTypeViewModel, validFilter.PageNumber, validFilter.PageSize);           
        }
    }
}
