using Application.Common;
using Application.Common.Interfaces;
using Application.Common.Mediator;
using Application.Dtos.Auth;
using Application.Interfaces;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Auth
{
    public class LoginUserCommandHandler(IApplicationDbContext context, ITokenProvider tokenProvider) : IRequestHandler<LoginUserCommand, Result<TokenResponseDto>>
    {
        public async Task<Result<TokenResponseDto>> Handle(LoginUserCommand request, CancellationToken cancellationToken)
        {
            var loginUser = request.Request;

            var user = await context.Users
                .FirstOrDefaultAsync(u => u.Email.ToLower() == loginUser.Email.ToLower(), cancellationToken);

            if (user is null)
                return Result<TokenResponseDto>.Failure("Invalid login credentials.", 401);

            if (!IsPasswordCorrect(user, loginUser.Password))
                return Result<TokenResponseDto>.Failure("Invalid login credentials.", 401);

            var tokenResponse = tokenProvider.CreateTokenResponse(user);
            await context.SaveChangesAsync(cancellationToken);

            return Result<TokenResponseDto>.Success(tokenResponse);
        }

        private static bool IsPasswordCorrect(User user, string password)
        {
            return new PasswordHasher<User>().VerifyHashedPassword(user, user.PasswordHash, password) == PasswordVerificationResult.Success;
        }
    }
}
