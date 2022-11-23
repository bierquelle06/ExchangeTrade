using Application.Features.Currency.Commands.CreateCurrency;
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

namespace Application.Features.Currency.Commands.CreateCurrency
{
    public class CreateCurrencyCommandValidator : AbstractValidator<CreateCurrencyCommand>
    {
        private readonly ICurrencyRepositoryAsync _repository;

        public CreateCurrencyCommandValidator(ICurrencyRepositoryAsync repository)
        {
            this._repository = repository;

            RuleFor(p => p.Name)
               .NotEmpty().WithMessage("{PropertyName} is required.")
               .NotNull()
               .MaximumLength(100).WithMessage("{PropertyName} must not exceed 100 characters.");

            RuleFor(p => p.Code)
             .NotEmpty().WithMessage("{PropertyName} is required.")
             .NotNull()
             .MaximumLength(150).WithMessage("{PropertyName} must not exceed 150 characters.")
             .MustAsync(IsUniqueCode).WithMessage("{PropertyName} already exists.");
        }

        private async Task<bool> IsUniqueCode(string code, CancellationToken cancellationToken)
        {
            var result = await _repository.Find(p => p.Code == code && p.IsDelete == false).AnyAsync();
            return !result;
        }
    }
}
