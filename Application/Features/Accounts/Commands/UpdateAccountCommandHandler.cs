﻿using Application.Common;
using Application.Common.Interfaces;
using Application.Common.Mediator;
using Application.Dtos.Accounts;
using Application.Dtos.Transactions;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Accounts
{
    public class UpdateAccountCommandHandler(IApplicationDbContext context) : IRequestHandler<UpdateAccountCommand, Result<AccountDto>>
    {
        public async Task<Result<AccountDto>> Handle(UpdateAccountCommand request, CancellationToken cancellationToken)
        {
            var accountId = request.Id;
            if (accountId == Guid.Empty)
                return Result<AccountDto>.Failure("Account ID cannot be empty.", 400);

            var account = await context.Accounts
                .Include(a => a.Transactions)
                .FirstOrDefaultAsync(a => a.Id == accountId, cancellationToken);
            if (account is null)
                return Result<AccountDto>.Failure("Account not found.", 404);

            var accountDto = request.Request;
            if (request.UserId != account.UserId)
                return Result<AccountDto>.Failure("Unauthorized access.", 401);

            if (string.IsNullOrWhiteSpace(accountDto.Name) 
                || string.IsNullOrWhiteSpace(accountDto.Currency))
                return Result<AccountDto>.Failure("Invalid account data provided for update.", 400);

            account.Name = accountDto.Name;
            account.Balance = accountDto.Balance;
            account.Currency = accountDto.Currency;

            await context.SaveChangesAsync(cancellationToken);

            var updatedAccountDto = new AccountDto
            (
                Id: account.Id,
                Name: account.Name,
                Balance: account.Balance,
                Currency: account.Currency,
                CreatedAt: account.CreatedAt,
                Transactions: account.Transactions?.Select(t => new TransactionDto
                (
                    Id: t.Id,
                    AccountId: t.AccountId,
                    CategoryId: t.CategoryId ?? Guid.Empty,
                    Amount: t.Amount,
                    Note: t.Note,
                    TransactionDate: t.TransactionDate,
                    Type: t.Type
                )).ToList() ?? new List<TransactionDto>()
            );

            return Result<AccountDto>.Success(updatedAccountDto);
        }
    }
}
