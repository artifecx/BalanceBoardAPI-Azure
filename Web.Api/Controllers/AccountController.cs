using Application.Common.Mediator;
using Application.Dtos.Accounts;
using Application.Features.Accounts;
using Infrastructure.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Web.Api.Extensions;

namespace Web.Api.Controllers
{
    [Route("api/v1/accounts")]
    [ApiController]
    [Authorize]
    public class AccountController(ISender Mediator) : ControllerBase
    {
        // GET: api/accounts
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await Mediator.Send(new GetAccountsQuery(User.GetUserId()));
            return this.HandleResult(result);
        }

        // GET api/accounts/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var result = await Mediator.Send(new GetAccountByIdQuery(id, User.GetUserId()));
            return this.HandleResult(result);
        }

        // POST api/accounts
        [HttpPost]
        public async Task<IActionResult> Create(UpsertAccountDto request)
        {
            SetUserIdInRequest(request);
            var result = await Mediator.Send(new CreateAccountCommand(request));
            return this.HandleResult(result);
        }

        // PUT api/accounts/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, UpsertAccountDto request)
        {
            SetUserIdInRequest(request);
            var result = await Mediator.Send(new UpdateAccountCommand(id, request));
            return this.HandleResult(result);
        }

        // DELETE api/accounts/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await Mediator.Send(new DeleteAccountCommand(id, User.GetUserId()));
            return this.HandleDeleteResult(result);
        }

        private void SetUserIdInRequest(UpsertAccountDto request)
        {
            request.UserId = User.GetUserId();
        }
    }
}
