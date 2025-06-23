using Application.Common;
using Application.Common.Mediator;

namespace Application.Features.Transactions
{
    public sealed record DeleteTransactionCommand(Guid Id, Guid UserId) : IRequest<Result<string>>;
}
