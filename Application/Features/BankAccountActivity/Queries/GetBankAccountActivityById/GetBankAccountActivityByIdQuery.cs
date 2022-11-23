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

namespace Application.Features.BankAccountActivity.Queries.GetBankAccountActivityById
{
    /// <summary>
    /// GetBankAccountActivityByIdQuery
    /// </summary>
    public class GetBankAccountActivityByIdQuery : IRequest<Response<Domain.Entities.BankAccountActivity.BankAccountActivity>>
    {
        public int Id { get; set; }

        /// <summary>
        /// GetBankAccountActivityByIdQuery -> Handler
        /// </summary>
        public class GetBankAccountActivityByIdQueryHandler : IRequestHandler<GetBankAccountActivityByIdQuery, Response<Domain.Entities.BankAccountActivity.BankAccountActivity>>
        {
            private readonly IBankAccountActivityRepositoryAsync _repository;
            
            private readonly IBankAccountRepositoryAsync _bankAccountRepository;
            
            public GetBankAccountActivityByIdQueryHandler(IBankAccountActivityRepositoryAsync repository,
                IBankAccountRepositoryAsync bankAccountRepository)
            {
                this._repository = repository;
                this._bankAccountRepository = bankAccountRepository;
            }

            public async Task<Response<Domain.Entities.BankAccountActivity.BankAccountActivity>> Handle(GetBankAccountActivityByIdQuery query, 
                CancellationToken cancellationToken)
            {
                var entity = await _repository.GetByIdAsync(query.Id);

                if (entity == null) 
                    throw new ApiException(BusinessExceptions.NotFound);

                if (entity.IsDelete.HasValue && entity.IsDelete.Value)
                    throw new ApiException(BusinessExceptions.DeletedRecord);

                return new Response<Domain.Entities.BankAccountActivity.BankAccountActivity>(entity);
            }
        }
    }
}
