using Application.Common;
using Application.Common.Mediator;
using Application.Dtos.Accounts;

namespace Application.Features.Accounts
{
    public sealed record CreateAccountCommand(UpsertAccountDto Request) : IRequest<Result<AccountDto>>;
}
