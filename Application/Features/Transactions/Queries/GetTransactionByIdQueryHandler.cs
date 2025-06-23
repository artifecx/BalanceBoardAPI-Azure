using Application.Common;
using Application.Common.Interfaces;
using Application.Common.Mediator;
using Application.Dtos.Transactions;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Transactions
{
    public class GetTransactionByIdQueryHandler(IApplicationDbContext context) : IRequestHandler<GetTransactionByIdQuery, Result<TransactionDto>>
    {
        public async Task<Result<TransactionDto>> Handle(GetTransactionByIdQuery request, CancellationToken cancellationToken)
        {
            var userId = request.UserId;
            var transactionId = request.Id;

            if (transactionId == Guid.Empty || userId == Guid.Empty)
                return Result<TransactionDto>.Failure("Unable to process request, missing IDs.", 400);

            var transaction = await context.Transactions
                .AsNoTracking()
                .FirstOrDefaultAsync(t => t.Id == transactionId && t.UserId == userId, cancellationToken);

            if (transaction is null)
                return Result<TransactionDto>.Failure("Transaction not found.", 404);

            var transactionDto = new TransactionDto
            (
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
