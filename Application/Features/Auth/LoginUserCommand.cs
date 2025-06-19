using Application.Common;
using Application.Common.Mediator;
using Application.Dtos;

namespace Application.Features.Auth
{
    public sealed record LoginUserCommand(LoginUserDto Request) : IRequest<Result<string>>;
}
