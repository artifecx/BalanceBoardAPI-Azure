using Application.Common;
using Application.Common.Mediator;
using Application.Dtos.Transactions;

namespace Application.Features.Transactions
{
    public sealed record CreateTransactionCommand(UpsertTransactionDto Request) : IRequest<Result<TransactionDto>>;
}
