﻿using Application.Common;
using Application.Common.Mediator;
using Application.Dtos.Auth;

namespace Application.Features.Auth
{
    public sealed record RegisterUserCommand(RegisterUserDto Request) : IRequest<Result<UserDto>>;
}
