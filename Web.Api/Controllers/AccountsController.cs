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
        // GET: api/accounts
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await mediator.Send(new GetAccountsQuery(User.GetUserId()));
            return this.HandleResult(result);
        }

        // GET api/accounts/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var result = await mediator.Send(new GetAccountByIdQuery(id, User.GetUserId()));
            return this.HandleResult(result);
        }

        // POST api/accounts
        [HttpPost]
        public async Task<IActionResult> Create(UpsertAccountDto request, IValidator<UpsertAccountDto> validator)
        {
            var validationResult = await this.ValidateRequest(request, validator);
            if (validationResult != null)
                return validationResult;

            SetUserIdInRequest(request);
            var result = await mediator.Send(new CreateAccountCommand(request));
            return this.HandleResult(result);
        }

        // PUT api/accounts/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, UpsertAccountDto request, IValidator<UpsertAccountDto> validator)
        {
            var validationResult = await this.ValidateRequest(request, validator);
            if (validationResult != null)
                return validationResult;

            SetUserIdInRequest(request);
            var result = await mediator.Send(new UpdateAccountCommand(id, request));
            return this.HandleResult(result);
        }

        // DELETE api/accounts/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await mediator.Send(new DeleteAccountCommand(id, User.GetUserId()));
            return this.HandleDeleteResult(result);
        }

        private void SetUserIdInRequest(UpsertAccountDto request)
        {
            request.UserId = User.GetUserId();
        }
    }
}
