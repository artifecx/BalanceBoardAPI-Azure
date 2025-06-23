using Application.Common;
using Application.Common.Interfaces;
using Application.Common.Mediator;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Transactions
{
    public class DeleteTransactionCommandHandler(IApplicationDbContext context) : IRequestHandler<DeleteTransactionCommand, Result<string>>
    {
        public async Task<Result<string>> Handle(DeleteTransactionCommand request, CancellationToken cancellationToken)
        {
            var userId = request.UserId;
            var transactionId = request.Id;

            var transaction = await context.Transactions
                .Include(t => t.Account)
                .FirstOrDefaultAsync(t => t.Id == transactionId && t.UserId == userId, cancellationToken);
            if (transaction is null)
                return Result<string>.Failure("Transaction not found.", 404);

            var amount = transaction.Amount;
            decimal signedAmount = transaction.Type == TransactionType.Income ? amount : -amount;
            transaction.Account.Balance -= signedAmount;

            context.Transactions.Remove(transaction);
            await context.SaveChangesAsync(cancellationToken);

            return Result<string>.Success("Successfully deleted transaction.");
        }
    }
}
