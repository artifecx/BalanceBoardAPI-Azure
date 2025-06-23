using Domain.Abstractions;

namespace Domain.Entities;

public sealed class User : BaseEntity
{
    public string Username { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string PasswordHash { get; set; } = null!;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public string? RefreshToken { get; set; } = string.Empty;
    public DateTime? RefreshTokenExpiry { get; set; }

    public ICollection<Account>? Accounts { get; private set; }
    public ICollection<Category>? Categories { get; private set; }
    public ICollection<Transaction>? Transactions { get; private set; }
}
