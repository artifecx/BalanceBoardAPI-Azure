using Application.Common;
using Application.Common.Mediator;

namespace Application.Features.Accounts
{
    public sealed record DeleteAccountCommand(Guid Id, Guid UserId) : IRequest<Result<string>>;
}
