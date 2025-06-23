using Application.Dtos.Accounts;
using FluentValidation;

namespace Application.Validators.Accounts
{
    internal sealed class UpsertAccountDtoValidator : AbstractValidator<UpsertAccountDto>
    {
        public UpsertAccountDtoValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                    .WithMessage("Name is required.")
                .Length(3, 100)
                    .WithMessage("Name must be between 3 and 100 characters long.");

            RuleFor(x => x.Balance)
                .NotNull()
                    .WithMessage("Balance is required.")
                .PrecisionScale(18, 2, true)
                    .WithMessage("Balance must have a maximum of 18 digits and 2 decimal places.");
        }
    }
}
