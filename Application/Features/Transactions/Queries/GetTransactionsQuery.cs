using Application.Common;
using Application.Common.Mediator;
using Application.Dtos.Transactions;

namespace Application.Features.Transactions
{
    public sealed record GetTransactionsQuery(Guid UserId) : IRequest<Result<List<TransactionDto>>>;
}
