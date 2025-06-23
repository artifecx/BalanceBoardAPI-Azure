using Application.Common.Mediator;
using Application.Dtos.Transactions;
using Application.Features.Transactions;
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
    public class TransactionsController(ISender mediator) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await mediator.Send(new GetTransactionsQuery(User.GetUserId()));
            return this.HandleResult(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var result = await mediator.Send(new GetTransactionByIdQuery(id, User.GetUserId()));
            return this.HandleResult(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create(UpsertTransactionDto request, IValidator<UpsertTransactionDto> validator)
        {
            var validationResult = await this.ValidateRequest(request, validator);
            if (validationResult != null)
                return validationResult;

            SetUserIdInRequest(request);
            var result = await mediator.Send(new CreateTransactionCommand(request));
            return this.HandleResult(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, UpsertTransactionDto request, IValidator<UpsertTransactionDto> validator)
        {
            var validationResult = await this.ValidateRequest(request, validator);
            if (validationResult != null)
                return validationResult;

            SetUserIdInRequest(request);
            var result = await mediator.Send(new UpdateTransactionCommand(id, request));
            return this.HandleResult(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await mediator.Send(new DeleteTransactionCommand(id, User.GetUserId()));
            return this.HandleDeleteResult(result);
        }

        private void SetUserIdInRequest(UpsertTransactionDto request)
        {
            request.UserId = User.GetUserId();
        }
    }
}
