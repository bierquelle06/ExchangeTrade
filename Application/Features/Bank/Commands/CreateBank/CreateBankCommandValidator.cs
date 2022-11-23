using Application.Features.Bank.Commands.CreateBank;
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

namespace Application.Features.Bank.Commands.CreateBank
{
    public class CreateBankCommandValidator : AbstractValidator<CreateBankCommand>
    {
        private readonly IBankRepositoryAsync _repository;
        public CreateBankCommandValidator(IBankRepositoryAsync repository)
        {
            this._repository = repository;

            RuleFor(p => p.Name)
               .NotEmpty().WithMessage("{PropertyName} is required.")
               .NotNull()
               .MaximumLength(100).WithMessage("{PropertyName} must not exceed 100 characters.");
        }
    }
}
