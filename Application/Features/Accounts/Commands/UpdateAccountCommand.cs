using Application.Common;
using Application.Common.Mediator;
using Application.Dtos.Accounts;

namespace Application.Features.Accounts
{
    public sealed record UpdateAccountCommand(Guid Id, UpsertAccountDto Request) : IRequest<Result<AccountDto>>;
}
