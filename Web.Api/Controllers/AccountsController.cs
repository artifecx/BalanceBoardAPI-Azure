using Application.Common.Mediator;
using Application.Dtos.Accounts;
using Application.Features.Accounts;
using FluentValidation;
using Infrastructure.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Web.Api.Extensions;

namespace Web.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    [Authorize]
    public class AccountsController(ISender mediator) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await mediator.Send(new GetAccountsQuery(User.GetUserId()));
            return this.HandleResult(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var result = await mediator.Send(new GetAccountByIdQuery(id, User.GetUserId()));
            return this.HandleResult(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create(UpsertAccountDto request, IValidator<UpsertAccountDto> validator)
        {
            var validationResult = await this.ValidateRequest(request, validator);
            if (validationResult != null)
                return validationResult;

            var result = await mediator.Send(new CreateAccountCommand(User.GetUserId(), request));
            return this.HandleResult(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, UpsertAccountDto request, IValidator<UpsertAccountDto> validator)
        {
            var validationResult = await this.ValidateRequest(request, validator);
            if (validationResult != null)
                return validationResult;

            var result = await mediator.Send(new UpdateAccountCommand(id, User.GetUserId(), request));
            return this.HandleResult(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await mediator.Send(new DeleteAccountCommand(id, User.GetUserId()));
            return this.HandleDeleteResult(result);
        }
    }
}
