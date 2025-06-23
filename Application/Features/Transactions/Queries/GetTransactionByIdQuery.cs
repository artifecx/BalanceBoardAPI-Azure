using Application.Common;
using Application.Common.Mediator;
using Application.Dtos.Transactions;

namespace Application.Features.Transactions
{
    public sealed record GetTransactionByIdQuery(Guid Id, Guid UserId) : IRequest<Result<TransactionDto>>;
}
