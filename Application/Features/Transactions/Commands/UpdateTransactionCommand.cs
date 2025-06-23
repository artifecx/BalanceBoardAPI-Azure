using Application.Common;
using Application.Common.Mediator;
using Application.Dtos.Transactions;

namespace Application.Features.Transactions
{
    public sealed record UpdateTransactionCommand(Guid Id, UpsertTransactionDto Request) : IRequest<Result<TransactionDto>>;
}
