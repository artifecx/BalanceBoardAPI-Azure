using Application.Common;
using Application.Common.Interfaces;
using Application.Common.Mediator;
using Application.Interfaces;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Auth
{
    public class LoginUserCommandHandler(IApplicationDbContext context, ITokenProvider tokenProvider) : IRequestHandler<LoginUserCommand, Result<string>>
    {
        public async Task<Result<string>> Handle(LoginUserCommand request, CancellationToken cancellationToken)
        {
            var loginUser = request.Request;

            var user = await context.Users
                .FirstOrDefaultAsync(u => u.Email.ToLower() == loginUser.Email.ToLower(), cancellationToken);

            if (user is null)
            {
                return Result<string>.Failure("Invalid login credentials.");
            }

            if (!IsPasswordCorrect(user, loginUser.Password))
            {
                return Result<string>.Failure("Invalid login credentials.");
            }

            return Result<string>.Success(tokenProvider.Create(user));
        }

        private static bool IsPasswordCorrect(User user, string password)
        {
            return new PasswordHasher<User>().VerifyHashedPassword(user, user.PasswordHash, password) == PasswordVerificationResult.Success;
        }
    }
}
