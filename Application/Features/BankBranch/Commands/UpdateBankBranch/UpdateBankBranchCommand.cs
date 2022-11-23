using Application.Exceptions;
using Application.Branch.Enums;
using Application.Interfaces.Repositories;
using Application.Wrappers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.BankBranch.Commands.UpdateBankBranch
{
    public class UpdateBankBranchCommand : IRequest<Response<int>>
    {
        public int Id { get; }

        public string Name { get; set; }

        public int BankId { get; set; }

        public UpdateBankBranchCommand(int id)
        {
            this.Id = id;
        }
    }

    public class UpdateBankBranchCommandHandler : IRequestHandler<UpdateBankBranchCommand, Response<int>>
    {
        private readonly IBankBranchRepositoryAsync _repository;

        public UpdateBankBranchCommandHandler(IBankBranchRepositoryAsync BranchRepository)
        {
            this._repository = BranchRepository;
        }

        public async Task<Response<int>> Handle(UpdateBankBranchCommand command, CancellationToken cancellationToken)
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
                entity.BankId = command.BankId;
                
                await this._repository.UpdateAsync(entity);

                return new Response<int>(entity.Id);
            }
        }
    }
}
