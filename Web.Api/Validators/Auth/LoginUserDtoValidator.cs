using Application.Dtos.Auth;
using FluentValidation;

namespace Application.Validators.Auth
{
    internal sealed class LoginUserDtoValidator : AbstractValidator<LoginUserDto>
    {
        public LoginUserDtoValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty()
                    .WithMessage("Email is required")
                .EmailAddress()
                    .WithMessage("Invalid email format");

            RuleFor(x => x.Password)
                .NotEmpty()
                    .WithMessage("Password is required");
        }
    }
}
