using Application.Features.Integrator.Commands.CreateIntegrator;
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

namespace Application.Features.Integrator.Commands.CreateIntegrator
{
    public class CreateIntegratorCommandValidator : AbstractValidator<CreateIntegratorCommand>
    {
        private readonly IIntegratorRepositoryAsync _integratorRepository;

        public CreateIntegratorCommandValidator(IIntegratorRepositoryAsync integratorRepository)
        {
            this._integratorRepository = integratorRepository;

            RuleFor(p => p.Name)
               .NotEmpty().WithMessage("{PropertyName} is required.")
               .NotNull()
               .MaximumLength(150).WithMessage("{PropertyName} must not exceed 150 characters.");

            RuleFor(p => p.Code)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull()
                .MaximumLength(150).WithMessage("{PropertyName} must not exceed 150 characters.")
                .MustAsync(IsUniqueCode).WithMessage("{PropertyName} already exists.");
           
            RuleFor(p => p.UserName)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull()
                .MaximumLength(150).WithMessage("{PropertyName} must not exceed 150 characters.");

            RuleFor(p => p.Password)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull()
                .MaximumLength(150).WithMessage("{PropertyName} must not exceed 150 characters.");
        }

        private async Task<bool> IsUniqueCode(string code, CancellationToken cancellationToken)
        {
            var result = await _integratorRepository.Find(p => p.Code == code && p.IsDelete == false).AnyAsync();
            return !result;
        }
    }
}
