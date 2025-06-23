using Domain.Abstractions;

namespace Domain.Entities;

public sealed class Account : BaseEntity
{
    public Guid UserId { get; set; }
    public string Name { get; set; } = null!; //  "BDO Savings"
    public decimal Balance { get; set; } = 0.0m;
    public string Currency { get; set; } = "PHP"; // Default currency
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public ICollection<Transaction>? Transactions { get; private set; }
    public User User { get; private set; } = null!;
}
