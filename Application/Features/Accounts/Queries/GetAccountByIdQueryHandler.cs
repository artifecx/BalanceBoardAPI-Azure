using Application.Common;
using Application.Common.Interfaces;
using Application.Common.Mediator;
using Application.Dtos.Accounts;
using Application.Dtos.Transactions;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Accounts
{
    public class GetAccountByIdQueryHandler(IApplicationDbContext context) : IRequestHandler<GetAccountByIdQuery, Result<AccountDto>>
    {
        public async Task<Result<AccountDto>> Handle(GetAccountByIdQuery request, CancellationToken cancellationToken)
        {
            var accountId = request.Id;
            var userId = request.UserId;

            if (accountId == Guid.Empty || userId == Guid.Empty)
                return Result<AccountDto>.Failure("Unable to process request, missing IDs.", 400);

            var account = await context.Accounts
                .Include(a => a.Transactions)
                .AsNoTracking()
                .FirstOrDefaultAsync(a => a.Id == accountId && a.UserId == userId, cancellationToken);

            if (account is null)
                return Result<AccountDto>.Failure("Account not found.", 404);

            var transactionDtos = account.Transactions?.Select(t => new TransactionDto
            (
                Id: t.Id,
                AccountId: t.AccountId,
                CategoryId: t.CategoryId ?? Guid.Empty,
                Amount: t.Amount,
                Note: t.Note,
                TransactionDate: t.TransactionDate,
                Type: t.Type
            )).ToList() ?? new List<TransactionDto>();

            var accountDto = new AccountDto
            (
                Id: account.Id,
                Name: account.Name,
                Balance: account.Balance,
                Currency: account.Currency,
                CreatedAt: account.CreatedAt,
                Transactions: transactionDtos
            );

            return Result<AccountDto>.Success(accountDto);
        }
    }
}
