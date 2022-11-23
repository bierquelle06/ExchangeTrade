using Application.Exceptions;
using Application.Interfaces.Repositories;
using Application.Wrappers;

using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.BankBranch.Queries.GetBankBranchById
{
    /// <summary>
    /// GetBankBranchByIdQuery
    /// </summary>
    public class GetBankBranchByIdQuery : IRequest<Response<Domain.Entities.BankBranch.BankBranch>>
    {
        public int Id { get; set; }

        /// <summary>
        /// GetBranchByIdQuery -> Handler
        /// </summary>
        public class GetBankBranchByIdQueryHandler : IRequestHandler<GetBankBranchByIdQuery, Response<Domain.Entities.BankBranch.BankBranch>>
        {
            private readonly IBankBranchRepositoryAsync _bankBranchRepository;

            public GetBankBranchByIdQueryHandler(IBankBranchRepositoryAsync bankBranchRepository)
            {
                _bankBranchRepository = bankBranchRepository;
            }

            public async Task<Response<Domain.Entities.BankBranch.BankBranch>> Handle(GetBankBranchByIdQuery query, CancellationToken cancellationToken)
            {
                var entity = await _bankBranchRepository.GetByIdAsync(query.Id);

                if (entity == null) 
                    throw new ApiException(BusinessExceptions.NotFound);

                if (entity.IsDelete.HasValue && !entity.IsDelete.Value)
                    throw new ApiException(BusinessExceptions.DeletedRecord);

                return new Response<Domain.Entities.BankBranch.BankBranch>(entity);
            }
        }
    }
}
