using Application.Common;
using Application.Common.Interfaces;
using Application.Common.Mediator;
using Application.Dtos.Accounts;
using Application.Dtos.Transactions;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Accounts
{
    public class GetAccountsQueryHandler(IApplicationDbContext context) : IRequestHandler<GetAccountsQuery, Result<List<AccountDto>>>
    {
        public async Task<Result<List<AccountDto>>> Handle(GetAccountsQuery request, CancellationToken cancellationToken)
        {
            if(request.UserId == Guid.Empty)
            {
                return Result<List<AccountDto>>.Failure("Unable to process request, User ID is not provided.");
            }

            var accounts = await context.Accounts
                .Where(a => a.UserId == request.UserId)
                .Include(a => a.Transactions)
                .Select(a => new AccountDto
                (
                    a.Id,
                    a.Name,
                    a.Balance,
                    a.Currency,
                    a.CreatedAt,
                    a.Transactions != null && a.Transactions.Count > 0 
                        ? a.Transactions.Select(t => new TransactionDto
                        (
                            t.Id,
                            t.AccountId,
                            t.CategoryId ?? Guid.Empty,
                            t.Amount,
                            t.Note,
                            t.TransactionDate,
                            t.Type
                        )).ToList() 
                        : new List<TransactionDto>()
                ))
                .AsNoTracking()
                .ToListAsync(cancellationToken);

            if (accounts is null || accounts.Count == 0)
            {
                return Result<List<AccountDto>>.Failure("No accounts found for the specified user.");
            }

            return Result<List<AccountDto>>.Success(accounts);
        }
    }
}
