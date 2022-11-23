using Application.Currency.Enums;
using Application.Exceptions;
using Application.Interfaces.Repositories;
using Application.Wrappers;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Currency.Commands.UpdateCurrency
{
    public class UpdateCurrencyCommand : IRequest<Response<int>>
    {
        public int Id { get; }

        public string Name { get; set; }

        public string Code { get; set; }

        public UpdateCurrencyCommand(int id)
        {
            this.Id = id;
        }
    }

    public class UpdateCurrencyCommandHandler : IRequestHandler<UpdateCurrencyCommand, Response<int>>
    {
        private readonly ICurrencyRepositoryAsync _repository;
        private readonly IMapper _mapper;

        public UpdateCurrencyCommandHandler(ICurrencyRepositoryAsync repository, IMapper mapper)
        {
            this._repository = repository;
            this._mapper = mapper;
        }

        public async Task<Response<int>> Handle(UpdateCurrencyCommand command, CancellationToken cancellationToken)
        {
            var entity = await this._repository.GetByIdAsync(command.Id);

            if (entity == null)
            {
                throw new ApiException(BusinessExceptions.NotFound);
            }
            else
            {
                entity.Name = command.Name;
                entity.Code = command.Code;
                
                await this._repository.UpdateAsync(entity);

                return new Response<int>(entity.Id);
            }
        }
    }
}
