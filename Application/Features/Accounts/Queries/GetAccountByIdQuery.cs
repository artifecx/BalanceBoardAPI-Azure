using Application.Common;
using Application.Common.Mediator;
using Application.Dtos.Accounts;

namespace Application.Features.Accounts
{
    public sealed record GetAccountByIdQuery(Guid Id, Guid UserId) : IRequest<Result<AccountDto>>;
}
