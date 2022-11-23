using Application.BankActivity.Enums;
using Application.Features.BankAccountActivity.Commands.CreateBankAccountActivity;
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

namespace Application.Features.BankAccountActivity.Commands.CreateBankAccountActivity
{
    public class CreateBankAccountActivityCommandValidator : AbstractValidator<CreateBankAccountActivityCommand>
    {
        private readonly IBankAccountActivityRepositoryAsync _repository;
        
        public CreateBankAccountActivityCommandValidator(IBankAccountActivityRepositoryAsync repository)
        {
            this._repository = repository;

            RuleFor(p => p.CurrencyCode)
               .NotEmpty().WithMessage("{PropertyName} is required.")
               .NotNull()
               .MaximumLength(50).WithMessage("{PropertyName} must not exceed 50 characters.");

            RuleFor(p => p.ProcessID)
            .NotEmpty().WithMessage("{PropertyName} is required.")
            .NotNull();

            RuleFor(p => p.BankAccountId)
             .NotEmpty().WithMessage("{PropertyName} is required.")
             .NotNull();
        }
    }
}
