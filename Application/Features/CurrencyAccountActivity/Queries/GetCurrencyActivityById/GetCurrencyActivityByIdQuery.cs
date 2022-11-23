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

namespace Application.Features.CurrencyActivity.Queries.GetCurrencyActivityById
{
    /// <summary>
    /// GetCurrencyActivityByIdQuery
    /// </summary>
    public class GetCurrencyActivityByIdQuery : IRequest<Response<Domain.Entities.CurrencyActivity.CurrencyActivity>>
    {
        public int Id { get; set; }

        /// <summary>
        /// GetCurrencyActivityByIdQuery -> Handler
        /// </summary>
        public class GetCurrencyActivityByIdQueryHandler : IRequestHandler<GetCurrencyActivityByIdQuery, Response<Domain.Entities.CurrencyActivity.CurrencyActivity>>
        {
            private readonly ICurrencyActivityRepositoryAsync _repository;
            
            private readonly ICurrencyRepositoryAsync _currencyRepository;
            
            public GetCurrencyActivityByIdQueryHandler(ICurrencyActivityRepositoryAsync repository, ICurrencyRepositoryAsync CurrencyRepository)
            {
                this._repository = repository;
                this._currencyRepository = CurrencyRepository;
            }

            public async Task<Response<Domain.Entities.CurrencyActivity.CurrencyActivity>> Handle(GetCurrencyActivityByIdQuery query, CancellationToken cancellationToken)
            {
                var entity = await _repository.GetByIdAsync(query.Id);

                if (entity == null) 
                    throw new ApiException(BusinessExceptions.NotFound);

                if (entity.IsDelete.HasValue && entity.IsDelete.Value)
                    throw new ApiException(BusinessExceptions.DeletedRecord);

                return new Response<Domain.Entities.CurrencyActivity.CurrencyActivity>(entity);
            }
        }
    }
}
