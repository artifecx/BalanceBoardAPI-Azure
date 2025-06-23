using Application.Common;
using Application.Common.Interfaces;
using Application.Common.Mediator;
using Application.Dtos.Auth;
using Application.Interfaces;
using Domain.Entities;

namespace Application.Features.Auth
{
    public class RefreshTokenCommandHandler(IApplicationDbContext context, ITokenProvider tokenProvider) : IRequestHandler<RefreshTokenCommand, Result<TokenResponseDto>>
    {
        public async Task<Result<TokenResponseDto>> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
        {
            var refreshTokenRequest = request.Request;
            
            var user = await ValidateRefreshTokenAsync(refreshTokenRequest.UserId, refreshTokenRequest.RefreshToken);
            if(user is null)
                return Result<TokenResponseDto>.Failure("Invalid refresh token.", 401);

            var tokenResponse = tokenProvider.CreateTokenResponse(user);
            await context.SaveChangesAsync(cancellationToken);

            return Result<TokenResponseDto>.Success(tokenResponse);
        }

        private async Task<User?> ValidateRefreshTokenAsync(Guid userId, string refreshToken)
        {
            var user = await context.Users.FindAsync(userId);

            if (user is null
                || user.RefreshToken != refreshToken
                || user.RefreshTokenExpiry < DateTime.UtcNow)
            {
                return null;
            }

            return user;
        }
    }
}
