using Application.Dtos.Auth;
using Domain.Entities;

namespace Application.Interfaces;

public interface ITokenProvider
{
    TokenResponseDto CreateTokenResponse(User user);
}
