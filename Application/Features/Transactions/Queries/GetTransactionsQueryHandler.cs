using Application.Common;
using Application.Common.Interfaces;
using Application.Common.Mediator;
using Application.Dtos.Accounts;
using Application.Dtos.Transactions;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Transactions
{
    public class GetTransactionsQueryHandler(IApplicationDbContext context) : IRequestHandler<GetTransactionsQuery, Result<List<TransactionDto>>>
    {
        public async Task<Result<List<TransactionDto>>> Handle(GetTransactionsQuery request, CancellationToken cancellationToken)
        {
            var userId = request.UserId;

            if (userId == Guid.Empty)
                return Result<List<TransactionDto>>.Failure("Unable to process request, User ID is not provided.", 400);

            var transactions = await context.Transactions
                .Where(t => t.UserId == userId)
                .Select(a => new TransactionDto
                (
                    a.Id,
                    a.AccountId,
                    a.CategoryId ?? Guid.Empty,
                    a.Amount,
                    a.Note,
                    a.TransactionDate,
                    a.Type
                ))
                .AsNoTracking()
                .ToListAsync(cancellationToken);

            if (transactions is null || transactions.Count == 0)
                return Result<List<TransactionDto>>.Failure("No transactions found for the specified user.", 404);

            return Result<List<TransactionDto>>.Success(transactions);
        }
    }
}
