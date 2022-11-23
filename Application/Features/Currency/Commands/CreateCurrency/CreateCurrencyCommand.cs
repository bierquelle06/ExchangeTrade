using Application.Currency.Enums;
using Application.Interfaces.Repositories;
using Application.Wrappers;

using AutoMapper;

using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Currency.Commands.CreateCurrency
{
    /// <summary>
    /// Create Currency Command
    /// </summary>
    public partial class CreateCurrencyCommand : IRequest<Response<int>>
    {
        public string Name { get; }

        public string Code { get; }

        public CreateCurrencyCommand(string name, string code)
        {
            this.Name = name;
            this.Code = code;
        }

        public Domain.Entities.Currency.Currency CreateNewEntity()
        {
            return new Domain.Entities.Currency.Currency()
            {
                Name = this.Name,
                Code = this.Code
            };
        }
    }

    /// <summary>
    /// Create Currency Command -> Handler
    /// </summary>
    public class CreateCurrencyCommandHandler : IRequestHandler<CreateCurrencyCommand, Response<int>>
    {
        private readonly ICurrencyRepositoryAsync _currencyRepository;
        private readonly IMapper _mapper;

        public CreateCurrencyCommandHandler(ICurrencyRepositoryAsync CurrencyRepository, IMapper mapper)
        {
            _currencyRepository = CurrencyRepository;
            _mapper = mapper;
        }

        public async Task<Response<int>> Handle(CreateCurrencyCommand request, CancellationToken cancellationToken)
        {
            var entity = _mapper.Map<Domain.Entities.Currency.Currency>(request);

            await _currencyRepository.AddAsync(entity);

            return new Response<int>(entity.Id);
        }
    }
}
