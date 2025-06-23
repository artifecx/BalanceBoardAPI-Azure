using Application.Common;
using Application.Common.Interfaces;
using Application.Common.Mediator;
using Application.Dtos.Transactions;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Transactions
{
    public class CreateTransactionCommandHandler(IApplicationDbContext context) : IRequestHandler<CreateTransactionCommand, Result<TransactionDto>>
    {
        public async Task<Result<TransactionDto>> Handle(CreateTransactionCommand request, CancellationToken cancellationToken)
        {
            var newTransaction = request.Request;

            var userId = newTransaction.UserId;
            if (!userId.HasValue)
                return Result<TransactionDto>.Failure("Unauthorized access.", 401);

            var userExists = await context.Users
                .AnyAsync(u => u.Id == userId, cancellationToken);
            if (!userExists)
                return Result<TransactionDto>.Failure("User does not exist.", 404);

            var accountId = newTransaction.AccountId;
            var account = await context.Accounts
                .FirstOrDefaultAsync(a => a.Id == accountId && a.UserId == userId, cancellationToken);
            if (account is null)
                return Result<TransactionDto>.Failure("Account does not exist.", 404);

            var categoryId = newTransaction.CategoryId;
            if (categoryId.HasValue)
            {
                var categoryExists = await context.Categories
                    .AnyAsync(c => c.Id == categoryId && c.UserId == userId, cancellationToken);
                if (!categoryExists)
                    return Result<TransactionDto>.Failure("Category does not exist.", 404);
            }

            account.Balance = newTransaction.Type == TransactionType.Income
                ? account.Balance + newTransaction.Amount
                : account.Balance - newTransaction.Amount;

            var transaction = new Transaction
            {
                Id = Guid.NewGuid(),
                UserId = userId!.Value,
                AccountId = accountId,
                CategoryId = categoryId,
                Amount = newTransaction.Amount,
                Note = newTransaction.Note,
                Type = newTransaction.Type
            };

            context.Transactions.Add(transaction);
            await context.SaveChangesAsync(cancellationToken);

            var transactionDto = new TransactionDto(
                Id: transaction.Id,
                AccountId: transaction.AccountId,
                CategoryId: transaction.CategoryId ?? Guid.Empty,
                Amount: transaction.Amount,
                Note: transaction.Note,
                TransactionDate: transaction.TransactionDate,
                Type: transaction.Type
            );

            return Result<TransactionDto>.Success(transactionDto);
        }
    }
}
