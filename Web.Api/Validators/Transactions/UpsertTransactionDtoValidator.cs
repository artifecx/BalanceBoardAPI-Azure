using Application.Dtos.Transactions;
using FluentValidation;

namespace Web.Api.Validators.Transactions
{
    internal sealed class UpsertTransactionDtoValidator : AbstractValidator<UpsertTransactionDto>
    {
        public UpsertTransactionDtoValidator()
        {
            RuleFor(x => x.Amount)
                .NotNull()
                    .WithMessage("Amount is required.")
                .PrecisionScale(18, 2, true)
                    .WithMessage("Amount must have a maximum of 18 digits and 2 decimal places.");
        }
    }
}
