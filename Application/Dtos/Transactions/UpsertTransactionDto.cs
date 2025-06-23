using Domain.Entities;

namespace Application.Dtos.Transactions;

public class UpsertTransactionDto
{
    public Guid? UserId { get; set; }
    public Guid AccountId { get; set; }
    public Guid? CategoryId { get; set; }
    public decimal Amount { get; set; }
    public string Note { get; set; } = string.Empty;
    public TransactionType Type { get; set; } = TransactionType.Expense;
}
