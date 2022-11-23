using Application.Exceptions;
using Application.Interfaces.Repositories;
using Application.Wrappers;

using AutoMapper;

using MediatR;

using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.CurrencyActivity.Commands.UpdateCurrencyActivity
{
    public class UpdateCurrencyActivityCommand : IRequest<Response<int>>
    {
        public int Id { get; }
        public int CurrencyId { get; set; }
        public string Symbol { get; set; }
        public decimal Rate { get; set; }

        public UpdateCurrencyActivityCommand(int id)
        {
            this.Id = id;
        }
    }

    public class UpdateCurrencyActivityCommandHandler : IRequestHandler<UpdateCurrencyActivityCommand, Response<int>>
    {
        private readonly ICurrencyActivityRepositoryAsync _repository;

        private readonly ICurrencyRepositoryAsync _currencyRepository;

        private readonly IMapper _mapper;

        public UpdateCurrencyActivityCommandHandler(ICurrencyActivityRepositoryAsync repository,
            ICurrencyRepositoryAsync currencyRepository,
            IMapper mapper)
        {
            this._repository = repository;

            this._currencyRepository = currencyRepository;

            this._mapper = mapper;
        }

        public async Task<Response<int>> Handle(UpdateCurrencyActivityCommand command, CancellationToken cancellationToken)
        {
            var CurrencyEntity = await this._currencyRepository.GetByIdAsync(command.CurrencyId);
            if (CurrencyEntity == null)
            {
                throw new ApiException(BusinessExceptions.CurrencyError + " " + BusinessExceptions.NotFound);
            }

            var entity = await this._repository.GetByIdAsync(command.Id);

            if (entity == null)
            {
                throw new ApiException(BusinessExceptions.NotFound);
            }
            else
            {
                entity.CurrencyId = command.CurrencyId;

                entity.Rate = command.Rate;
                entity.Symbol = command.Symbol;

                await this._repository.UpdateAsync(entity);

                return new Response<int>(entity.Id);
            }
        }
    }
}
