using Application.Exceptions;
using Application.Interfaces.Repositories;
using Application.Wrappers;

using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Currency.Queries.GetCurrencyById
{
    /// <summary>
    /// GetCurrencyByIdQuery
    /// </summary>
    public class GetCurrencyByIdQuery : IRequest<Response<Domain.Entities.Currency.Currency>>
    {
        public int Id { get; set; }

        /// <summary>
        /// GetCurrencyByIdQuery -> Handler
        /// </summary>
        public class GetCurrencyByIdQueryHandler : IRequestHandler<GetCurrencyByIdQuery, Response<Domain.Entities.Currency.Currency>>
        {
            private readonly ICurrencyRepositoryAsync _repository;

            /// <summary>
            /// 
            /// </summary>
            /// <param name="CurrencyRepository"></param>
            public GetCurrencyByIdQueryHandler(ICurrencyRepositoryAsync CurrencyRepository)
            {
                _repository = CurrencyRepository;
            }

            /// <summary>
            /// 
            /// </summary>
            /// <param name="query"></param>
            /// <param name="cancellationToken"></param>
            /// <returns></returns>
            public async Task<Response<Domain.Entities.Currency.Currency>> Handle(GetCurrencyByIdQuery query, CancellationToken cancellationToken)
            {
                var result = await _repository.GetByIdAsync(query.Id);

                if (result == null) 
                    throw new ApiException(BusinessExceptions.NotFound);

                if (result.IsDelete.HasValue && !result.IsDelete.Value)
                    throw new ApiException(BusinessExceptions.DeletedRecord);

                return new Response<Domain.Entities.Currency.Currency>(result);
            }
        }
    }
}
