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

namespace Application.Features.BankAccountType.Commands.UpdateBankAccountType
{
    public class UpdateBankAccountTypeCommand : IRequest<Response<int>>
    {
        public int Id { get; }

        public string Name { get; set; }

        public UpdateBankAccountTypeCommand(int id)
        {
            this.Id = id;
        }
    }

    public class UpdateBankAccountTypeCommandHandler : IRequestHandler<UpdateBankAccountTypeCommand, Response<int>>
    {
        private readonly IBankAccountTypeRepositoryAsync _repository;
        private readonly IMapper _mapper;

        public UpdateBankAccountTypeCommandHandler(IBankAccountTypeRepositoryAsync repository, IMapper mapper)
        {
            this._repository = repository;
            this._mapper = mapper;
        }

        public async Task<Response<int>> Handle(UpdateBankAccountTypeCommand command, CancellationToken cancellationToken)
        {
            var entity = await this._repository.GetByIdAsync(command.Id);

            if (entity == null)
            {
                throw new ApiException(BusinessExceptions.NotFound);
            }
            else if (entity.IsDelete.HasValue && entity.IsDelete.Value)
            {
                throw new ApiException(BusinessExceptions.DeletedRecord);
            }
            else
            {
                entity.Name = command.Name;

                await this._repository.UpdateAsync(entity);

                return new Response<int>(entity.Id);
            }
        }
    }
}
