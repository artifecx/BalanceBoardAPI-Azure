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
            if (request.Id == Guid.Empty)
            {
                return Result<AccountDto>.Failure("Unable to process request, Account ID is not provided.");
            }

            var accountEntity = await context.Accounts
                .Include(a => a.Transactions)
                .AsNoTracking()
                .FirstOrDefaultAsync(a => a.Id == request.Id, cancellationToken);

            if (accountEntity is null)
                return Result<AccountDto>.Failure("Account not found.");

            if (accountEntity.UserId != request.UserId)
                return Result<AccountDto>.Failure("Unauthorized access to account.");

            var transactionDtos = accountEntity.Transactions?.Select(t => new TransactionDto
            (
                Id: t.Id,
                AccountId: t.AccountId,
                CategoryId: t.CategoryId,
                Amount: t.Amount,
                Note: t.Note,
                TransactionDate: t.TransactionDate,
                Type: t.Type
            )).ToList() ?? new List<TransactionDto>();

            var accountDto = new AccountDto
            (
                Id: accountEntity.Id,
                Name: accountEntity.Name,
                Balance: accountEntity.Balance,
                Currency: accountEntity.Currency,
                CreatedAt: accountEntity.CreatedAt,
                Transactions: transactionDtos
            );

            return Result<AccountDto>.Success(accountDto);
        }
    }
}
