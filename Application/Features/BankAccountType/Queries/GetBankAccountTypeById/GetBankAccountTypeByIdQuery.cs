using Application.Exceptions;
using Application.Interfaces.Repositories;
using Application.Wrappers;

using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.BankAccountType.Queries.GetBankAccountTypeById
{
    /// <summary>
    /// GetBankAccountTypeByIdQuery
    /// </summary>
    public class GetBankAccountTypeByIdQuery : IRequest<Response<Domain.Entities.BankAccountType.BankAccountType>>
    {
        public int Id { get; set; }

        /// <summary>
        /// GetBankAccountTypeByIdQuery -> Handler
        /// </summary>
        public class GetBankAccountTypeByIdQueryHandler : IRequestHandler<GetBankAccountTypeByIdQuery, Response<Domain.Entities.BankAccountType.BankAccountType>>
        {
            private readonly IBankAccountTypeRepositoryAsync _repository;

            public GetBankAccountTypeByIdQueryHandler(IBankAccountTypeRepositoryAsync BankAccountTypeRepository)
            {
                _repository = BankAccountTypeRepository;
            }

            public async Task<Response<Domain.Entities.BankAccountType.BankAccountType>> Handle(GetBankAccountTypeByIdQuery query, CancellationToken cancellationToken)
            {
                var result = await _repository.GetByIdAsync(query.Id);

                if (result == null) 
                    throw new ApiException(BusinessExceptions.NotFound);

                if (result.IsDelete.HasValue && result.IsDelete.Value)
                    throw new ApiException(BusinessExceptions.DeletedRecord);

                return new Response<Domain.Entities.BankAccountType.BankAccountType>(result);
            }
        }
    }
}
