using Application.Common;
using Application.Common.Mediator;
using Application.Dtos.Accounts;

namespace Application.Features.Accounts
{
    public sealed record GetAccountsQuery(Guid UserId) : IRequest<Result<List<AccountDto>>>;
}
