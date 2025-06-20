using Application.Dtos.Transactions;

namespace Application.Dtos.Accounts
{
    public sealed record AccountDto
    (
        Guid Id,
        string Name,
        decimal Balance,
        string Currency,
        DateTime CreatedAt,
        List<TransactionDto>? Transactions
    );
}
