using Application.Common;
using Application.Common.Interfaces;
using Application.Common.Mediator;
using Application.Dtos.Accounts;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Accounts
{
    public class CreateAccountCommandHandler(IApplicationDbContext context) : IRequestHandler<CreateAccountCommand, Result<AccountDto>>
    {
        public async Task<Result<AccountDto>> Handle(CreateAccountCommand request, CancellationToken cancellationToken)
        {
            var accountDto = request.Request;
            var userId = accountDto.UserId;

            if (!userId.HasValue)
                return Result<AccountDto>.Failure("Unauthorized access.", 401);

            var user = await context.Users
                .Include(u => u.Accounts)
                .FirstOrDefaultAsync(u => u.Id == userId, cancellationToken);
            if (user is null)
                return Result<AccountDto>.Failure("User does not exist.", 404);

            if(user.Accounts is not null && user.Accounts.Any(a => a.Name.ToLower() == accountDto.Name.ToLower()))
                return Result<AccountDto>.Failure("Account with same name exists.", 409);

            var account = new Account
            {
                UserId = userId!.Value,
                Name = accountDto.Name,
                Balance = accountDto.Balance,
                Currency = accountDto.Currency,
                CreatedAt = DateTime.UtcNow
            };

            context.Accounts.Add(account);
            await context.SaveChangesAsync(cancellationToken);

            var createdAccountDto = new AccountDto
            (
                Id: account.Id,
                Name: account.Name,
                Balance: account.Balance,
                Currency: account.Currency,
                CreatedAt: account.CreatedAt,
                Transactions: null
            );

            return Result<AccountDto>.Success(createdAccountDto);
        }
    }
}
