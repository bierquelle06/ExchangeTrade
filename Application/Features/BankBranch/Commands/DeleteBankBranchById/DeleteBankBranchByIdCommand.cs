using Application.Exceptions;
using Application.Interfaces.Repositories;
using Application.Wrappers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.BankBranch.Commands.DeleteBankBranchById
{
    /// <summary>
    /// DeleteBankBranchByIdCommand
    /// </summary>
    public class DeleteBankBranchByIdCommand : IRequest<Response<int>>
    {
        public int Id { get; set; }

        /// <summary>
        /// DeleteBankBranchByIdCommand -> Handler
        /// </summary>
        public class DeleteBankBranchByIdCommandHandler : IRequestHandler<DeleteBankBranchByIdCommand, Response<int>>
        {
            private readonly IBankBranchRepositoryAsync _branchRepository;

            public DeleteBankBranchByIdCommandHandler(IBankBranchRepositoryAsync BranchRepository)
            {
                _branchRepository = BranchRepository;
            }

            public async Task<Response<int>> Handle(DeleteBankBranchByIdCommand command, CancellationToken cancellationToken)
            {
                var entity = await _branchRepository.GetByIdAsync(command.Id);

                if (entity == null) 
                    throw new ApiException(BusinessExceptions.NotFound);
                
                await _branchRepository.DeleteSoftAsync(entity.Id);
                
                return new Response<int>(entity.Id);
            }
        }
    }
}
