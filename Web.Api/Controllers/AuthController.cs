using Application.Common.Mediator;
using Application.Dtos;
using Application.Dtos.Auth;
using Application.Features.Auth;
using Microsoft.AspNetCore.Mvc;
using Web.Api.Extensions;

namespace Web.Api.Controllers
{
    [Route("api/v1/auth")]
    [ApiController]
    public class AuthController(ISender Mediator) : ControllerBase
    {
        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterUserDto request)
        {
            var result = await Mediator.Send(new RegisterUserCommand(request));
            return this.HandleResult(result);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginUserDto request)
        {
            var result = await Mediator.Send(new LoginUserCommand(request));
            return this.HandleResult(result);
        }

        [HttpPost("refresh-token")]
        public async Task<IActionResult> RefreshToken(RefreshTokenRequestDto request)
        {
            var result = await Mediator.Send(new RefreshTokenCommand(request));
            return this.HandleResultWithAuthFallback(result);
        }
    }
}
