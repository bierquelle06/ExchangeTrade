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

namespace Application.Features.Bank.Queries.GetBankById
{
    /// <summary>
    /// GetBankByIdQuery
    /// </summary>
    public class GetBankByIdQuery : IRequest<Response<Domain.Entities.Bank.Bank>>
    {
        public int Id { get; set; }

        /// <summary>
        /// GetBankByIdQuery -> Handler
        /// </summary>
        public class GetBankByIdQueryHandler : IRequestHandler<GetBankByIdQuery, Response<Domain.Entities.Bank.Bank>>
        {
            private readonly IBankRepositoryAsync _repository;
            private readonly IBankBranchRepositoryAsync _branchRepository;

            public GetBankByIdQueryHandler(IBankRepositoryAsync repository, IBankBranchRepositoryAsync branchRepository)
            {
                this._repository = repository;
                this._branchRepository = branchRepository;
            }

            public async Task<Response<Domain.Entities.Bank.Bank>> Handle(GetBankByIdQuery query, CancellationToken cancellationToken)
            {
                var entity = await _repository.GetByIdAsync(query.Id);

                if (entity == null) 
                    throw new ApiException(BusinessExceptions.NotFound);

                if (entity.IsDelete.HasValue && entity.IsDelete.Value)
                    throw new ApiException(BusinessExceptions.DeletedRecord);

                return new Response<Domain.Entities.Bank.Bank>(entity);
            }
        }
    }
}
