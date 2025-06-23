using Application.Common;
using Application.Common.Interfaces;
using Application.Common.Mediator;
using Application.Dtos.Transactions;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Transactions
{
    public class UpdateTransactionCommandHandler(IApplicationDbContext context) : IRequestHandler<UpdateTransactionCommand, Result<TransactionDto>>
    {
        public async Task<Result<TransactionDto>> Handle(UpdateTransactionCommand request, CancellationToken cancellationToken)
        {
            var updateTransaction = request.Request;

            var userId = updateTransaction.UserId;
            if (!userId.HasValue)
                return Result<TransactionDto>.Failure("Unauthorized access.", 401);

            var transaction = await context.Transactions
                .Include(t => t.Account)
                .Include(t => t.Category)
                .FirstOrDefaultAsync(a => a.Id == request.Id && a.UserId == userId, cancellationToken);
            if (transaction is null)
                return Result<TransactionDto>.Failure("Transaction does not exist.", 404);

            if (!HasChanges(updateTransaction, transaction))
                return Result<TransactionDto>.Success(CreateTransactionDto(transaction));

            var newAccount = await context.Accounts
                .FirstOrDefaultAsync(a => a.Id == updateTransaction.AccountId && a.UserId == userId, cancellationToken);
            if (newAccount is null)
                return Result<TransactionDto>.Failure("Account does not exist.", 404);

            if (updateTransaction.CategoryId.HasValue)
            {
                var categoryExists = await context.Categories
                    .AnyAsync(c => c.Id == updateTransaction.CategoryId && c.UserId == userId, cancellationToken);
                if (!categoryExists)
                    return Result<TransactionDto>.Failure("Category does not exist.", 404);
            }

            var originalAccount = transaction.Account;
            var originalAmount = transaction.Amount;
            var originalType = transaction.Type;

            decimal signedOriginalAmount = originalType == TransactionType.Income ? originalAmount : -originalAmount;
            originalAccount.Balance -= signedOriginalAmount;

            decimal signedNewAmount = updateTransaction.Type == TransactionType.Income ? updateTransaction.Amount : -updateTransaction.Amount;
            newAccount.Balance += signedNewAmount;

            transaction.AccountId = updateTransaction.AccountId;
            transaction.CategoryId = updateTransaction.CategoryId;
            transaction.Amount = updateTransaction.Amount;
            transaction.Note = updateTransaction.Note;
            transaction.Type = updateTransaction.Type;

            await context.SaveChangesAsync(cancellationToken);

            return Result<TransactionDto>.Success(CreateTransactionDto(transaction));
        }

        private static bool HasChanges(UpsertTransactionDto original, Transaction update)
        {
            return original.AccountId != update.AccountId ||
                original.CategoryId != update.CategoryId ||
                original.Amount != update.Amount ||
                original.Note != update.Note ||
                original.Type != update.Type;
        }

        private static TransactionDto CreateTransactionDto(Transaction transaction)
        {
            return new TransactionDto(
                Id: transaction.Id,
                AccountId: transaction.AccountId,
                CategoryId: transaction.CategoryId ?? Guid.Empty,
                Amount: transaction.Amount,
                Note: transaction.Note,
                TransactionDate: transaction.TransactionDate,
                Type: transaction.Type
            );
        }
    }
}
