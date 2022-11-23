using Application.Features.BankAccountType.Commands.CreateBankAccountType;
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

namespace Application.Features.BankAccountType.Commands.CreateBankAccountType
{
    public class CreateBankAccountTypeCommandValidator : AbstractValidator<CreateBankAccountTypeCommand>
    {
        private readonly IBankAccountTypeRepositoryAsync _BankAccountTypeRepository;

        public CreateBankAccountTypeCommandValidator(IBankAccountTypeRepositoryAsync BankAccountTypeRepository)
        {
            this._BankAccountTypeRepository = BankAccountTypeRepository;

            RuleFor(p => p.Name)
               .NotEmpty().WithMessage("{PropertyName} is required.")
               .NotNull()
               .MaximumLength(100).WithMessage("{PropertyName} must not exceed 100 characters.");
        }
    }
}
