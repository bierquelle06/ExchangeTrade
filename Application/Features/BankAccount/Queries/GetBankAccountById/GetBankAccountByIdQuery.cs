using Application.Exceptions;
using Application.Interfaces.Repositories;
using Application.Wrappers;

using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.BankAccount.Queries.GetBankAccountById
{
    /// <summary>
    /// GetBankAccountByIdQuery
    /// </summary>
    public class GetBankAccountByIdQuery : IRequest<Response<Domain.Entities.BankAccount.BankAccount>>
    {
        public int Id { get; set; }

        /// <summary>
        /// GetBankAccountByIdQuery -> Handler
        /// </summary>
        public class GetBankAccountByIdQueryHandler : IRequestHandler<GetBankAccountByIdQuery, Response<Domain.Entities.BankAccount.BankAccount>>
        {
            private readonly IBankAccountRepositoryAsync _repository;
            
            private readonly IBankBranchRepositoryAsync _branchRepository;
            private readonly IIntegratorRepositoryAsync _integratorRepository;
            private readonly IBankRepositoryAsync _bankRepository;
            private readonly IBankAccountTypeRepositoryAsync _bankAccountTypeRepository;
            private readonly ICurrencyRepositoryAsync _currencyRepositoryAsync;

            public GetBankAccountByIdQueryHandler(IBankAccountRepositoryAsync repository,
                IBankRepositoryAsync bankRepository,
                IBankBranchRepositoryAsync branchRepository,
                IIntegratorRepositoryAsync integratorRepository,
                IBankAccountTypeRepositoryAsync bankAccountTypeRepository,
                ICurrencyRepositoryAsync currencyRepository)
            {
                this._repository = repository;
                this._bankRepository = bankRepository;
                this._branchRepository = branchRepository;
                this._integratorRepository = integratorRepository;
                this._bankAccountTypeRepository = bankAccountTypeRepository;
                this._currencyRepositoryAsync = currencyRepository;
            }

            public async Task<Response<Domain.Entities.BankAccount.BankAccount>> Handle(GetBankAccountByIdQuery query, CancellationToken cancellationToken)
            {
                var entity = await _repository.GetByIdAsync(query.Id);

                if (entity == null) 
                    throw new ApiException(BusinessExceptions.NotFound);

                if (entity.IsDelete.HasValue && entity.IsDelete.Value)
                    throw new ApiException(BusinessExceptions.DeletedRecord);

                return new Response<Domain.Entities.BankAccount.BankAccount>(entity);
            }
        }
    }
}
