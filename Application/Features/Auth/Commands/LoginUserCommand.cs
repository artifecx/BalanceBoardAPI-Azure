using Application.Common;
using Application.Common.Mediator;
using Application.Dtos;
using Application.Dtos.Auth;

namespace Application.Features.Auth
{
    public sealed record LoginUserCommand(LoginUserDto Request) : IRequest<Result<TokenResponseDto>>;
}
