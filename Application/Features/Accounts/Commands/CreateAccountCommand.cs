using Application.Common;
using Application.Common.Mediator;
using Application.Dtos.Accounts;

namespace Application.Features.Accounts
{
    public sealed record CreateAccountCommand(Guid UserId, UpsertAccountDto Request) : IRequest<Result<AccountDto>>;
}
