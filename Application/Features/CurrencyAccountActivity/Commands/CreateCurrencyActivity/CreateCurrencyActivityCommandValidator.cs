using Application.Interfaces.Repositories;
using FluentValidation;

namespace Application.Features.CurrencyActivity.Commands.CreateCurrencyActivity
{
    public class CreateCurrencyActivityCommandValidator : AbstractValidator<CreateCurrencyActivityCommand>
    {
        private readonly ICurrencyActivityRepositoryAsync _repository;
        
        public CreateCurrencyActivityCommandValidator(ICurrencyActivityRepositoryAsync repository)
        {
            this._repository = repository;

            RuleFor(p => p.Symbol)
               .NotEmpty().WithMessage("{PropertyName} is required.")
               .NotNull()
               .MaximumLength(50).WithMessage("{PropertyName} must not exceed 50 characters.");

            RuleFor(p => p.Rate)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull();

            RuleFor(p => p.CurrencyId)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull()
                .GreaterThan(0);
        }
    }
}
