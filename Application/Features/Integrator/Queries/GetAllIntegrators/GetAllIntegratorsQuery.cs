using Application.Features.Integrator.Queries.GetAllIntegrators;
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

namespace Application.Features.Integrator.Queries.GetAllIntegrators
{
    /// <summary>
    /// GetAllIntegratorsQuery
    /// </summary>
    public class GetAllIntegratorsQuery : IRequest<PagedResponse<IEnumerable<GetAllIntegratorsViewModel>>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }

    /// <summary>
    /// GetAllIntegratorsQuery -> Handler
    /// </summary>
    public class GetAllIntegratorsQueryHandler : IRequestHandler<GetAllIntegratorsQuery, PagedResponse<IEnumerable<GetAllIntegratorsViewModel>>>
    {
        private readonly IIntegratorRepositoryAsync _integratorRepository;
        private readonly IMapper _mapper;

        public GetAllIntegratorsQueryHandler(IIntegratorRepositoryAsync IntegratorRepository, IMapper mapper)
        {
            _integratorRepository = IntegratorRepository;
            _mapper = mapper;
        }

        public async Task<PagedResponse<IEnumerable<GetAllIntegratorsViewModel>>> Handle(GetAllIntegratorsQuery request, CancellationToken cancellationToken)
        {
            var validFilter = _mapper.Map<GetAllIntegratorsParameter>(request);

            var integrator = await _integratorRepository.GetPagedReponseAsync(validFilter.PageNumber, validFilter.PageSize, x => x.IsDelete == false);

            var integratorViewModel = _mapper.Map<IEnumerable<GetAllIntegratorsViewModel>>(integrator);

            return new PagedResponse<IEnumerable<GetAllIntegratorsViewModel>>(integratorViewModel, validFilter.PageNumber, validFilter.PageSize);           
        }
    }
}
