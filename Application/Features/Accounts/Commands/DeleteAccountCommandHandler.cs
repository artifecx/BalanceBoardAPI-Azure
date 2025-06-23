using Application.Common;
using Application.Common.Interfaces;
using Application.Common.Mediator;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Accounts
{
    public class DeleteAccountCommandHandler(IApplicationDbContext context) : IRequestHandler<DeleteAccountCommand, Result<string>>
    {
        public async Task<Result<string>> Handle(DeleteAccountCommand request, CancellationToken cancellationToken)
        {
            var accountId = request.Id;
            if (accountId == Guid.Empty)
                return Result<string>.Failure("Account ID cannot be empty.", 400);

            var account = await context.Accounts
                .FirstOrDefaultAsync(a => a.Id == accountId, cancellationToken);
            if (account is null)
                return Result<string>.Failure("Account not found.", 404);

            if (account.UserId != request.UserId)
                return Result<string>.Failure("Unauthorized access.", 401);

            context.Accounts.Remove(account);
            await context.SaveChangesAsync(cancellationToken);

            return Result<string>.Success("Account deleted successfully.");
        }
    }
}
