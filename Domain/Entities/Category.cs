using Domain.Abstractions;

namespace Domain.Entities;

public sealed class Category : BaseEntity
{
    public Guid UserId { get; set; }
    public string Name { get; set; } = null!; // "Groceries", "Salary"
    public TransactionType Type { get; set; } = TransactionType.Expense;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public ICollection<Transaction>? Transactions { get; set; }
    public User User { get; set; } = null!;
}
