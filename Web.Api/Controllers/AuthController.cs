using Application.Common.Mediator;
using Application.Dtos.Auth;
using Application.Features.Auth;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Web.Api.Extensions;

namespace Web.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class AuthController(ISender Mediator) : ControllerBase
    {
        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterUserDto request, IValidator<RegisterUserDto> validator)
        {
            var validationResult = await this.ValidateRequest(request, validator);
            if (validationResult != null)
                return validationResult;

            var result = await Mediator.Send(new RegisterUserCommand(request));
            return this.HandleResult(result);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginUserDto request, IValidator<LoginUserDto> validator)
        {
            var validationResult = await this.ValidateRequest(request, validator);
            if (validationResult != null)
                return validationResult;

            var result = await Mediator.Send(new LoginUserCommand(request));
            return this.HandleResult(result);
        }

        [HttpPost("refresh-token")]
        public async Task<IActionResult> RefreshToken(RefreshTokenRequestDto request)
        {
            var result = await Mediator.Send(new RefreshTokenCommand(request));
            return this.HandleResult(result);
        }
    }
}
