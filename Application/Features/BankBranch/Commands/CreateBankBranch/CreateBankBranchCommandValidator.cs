using Application.Features.BankBranch.Commands.CreateBankBranch;
using Application.Interfaces.Repositories;
using Domain.Entities;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.BankBranch.Commands.CreateBankBranch
{
    public class CreateBankBranchCommandValidator : AbstractValidator<CreateBankBranchCommand>
    {
        private readonly IBankBranchRepositoryAsync _bankBranchRepository;

        public CreateBankBranchCommandValidator(IBankBranchRepositoryAsync bankBranchRepository)
        {
            this._bankBranchRepository = bankBranchRepository;

            RuleFor(p => p.Name)
               .NotEmpty().WithMessage("{PropertyName} is required.")
               .NotNull()
               .MaximumLength(150).WithMessage("{PropertyName} must not exceed 150 characters.");

            RuleFor(p => p.BankId)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull();
        }

    }
}
