using Domain.Abstractions;

namespace Domain.Entities;

public sealed class Category : BaseEntity
{
    public Guid UserId { get; set; }
    public string Name { get; set; } = null!; // "Groceries", "Salary"
    public TransactionType Type { get; set; } = TransactionType.Expense;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public ICollection<Transaction>? Transactions { get; private set; }
    public User User { get; private set; } = null!;
}
