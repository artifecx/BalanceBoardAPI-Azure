using Application.Common.Mediator;
using Application.Dtos;
using Application.Features.Auth;
using Microsoft.AspNetCore.Mvc;

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
            if (!result.IsSuccess && result.Data is null)
            {
                return BadRequest(new { message = result.Error });
            }

            return Ok(result.Data);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginUserDto request)
        {
            var result = await Mediator.Send(new LoginUserCommand(request));
            if (!result.IsSuccess && result.Data is null)
            {
                return BadRequest(new { message = result.Error });
            }

            return Ok(result.Data);
        }
    }
}
