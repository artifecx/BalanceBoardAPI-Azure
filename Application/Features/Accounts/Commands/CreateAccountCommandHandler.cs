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

            if (accountDto.UserId == Guid.Empty)
                return Result<AccountDto>.Failure("Unauthorized access.", 401);

            var userId = accountDto.UserId;
            var userExists = await context.Users
                .AnyAsync(u => u.Id == userId, cancellationToken);
            if (!userExists)
                return Result<AccountDto>.Failure("User does not exist.", 404);

            var account = new Account
            {
                UserId = userId,
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
