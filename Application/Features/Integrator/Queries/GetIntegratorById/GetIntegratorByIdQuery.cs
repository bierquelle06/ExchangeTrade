using Application.Exceptions;
using Application.Interfaces.Repositories;
using Application.Wrappers;

using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Integrator.Queries.GetIntegratorById
{
    /// <summary>
    /// GetIntegratorByIdQuery
    /// </summary>
    public class GetIntegratorByIdQuery : IRequest<Response<Domain.Entities.Integrator.Integrator>>
    {
        public int Id { get; set; }

        /// <summary>
        /// GetIntegratorByIdQuery -> Handler
        /// </summary>
        public class GetIntegratorByIdQueryHandler : IRequestHandler<GetIntegratorByIdQuery, Response<Domain.Entities.Integrator.Integrator>>
        {
            private readonly IIntegratorRepositoryAsync _integratorRepository;

            public GetIntegratorByIdQueryHandler(IIntegratorRepositoryAsync integratorRepository)
            {
                _integratorRepository = integratorRepository;
            }

            public async Task<Response<Domain.Entities.Integrator.Integrator>> Handle(GetIntegratorByIdQuery query, CancellationToken cancellationToken)
            {
                var entity = await _integratorRepository.GetByIdAsync(query.Id);

                if (entity == null) 
                    throw new ApiException(BusinessExceptions.NotFound);

                if (entity.IsDelete.HasValue && entity.IsDelete.Value)
                    throw new ApiException(BusinessExceptions.DeletedRecord);

                return new Response<Domain.Entities.Integrator.Integrator>(entity);
            }
        }
    }
}
