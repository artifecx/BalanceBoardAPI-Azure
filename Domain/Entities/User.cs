using Domain.Abstractions;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public sealed class User : BaseEntity
    {
        [Required]
        [StringLength(100, MinimumLength = 3)]
        public string Username { get; set; } = null!;

        [Required]
        [EmailAddress]
        [StringLength(256, MinimumLength = 5)]
        public string Email { get; set; } = null!;

        [Required]
        [MinLength(6)]
        public string PasswordHash { get; set; } = null!;

        [Required]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public string? RefreshToken { get; set; } = string.Empty;

        public DateTime? RefreshTokenExpiry { get; set; }


        [InverseProperty(nameof(Account.User))]
        [DeleteBehavior(DeleteBehavior.NoAction)]
        public ICollection<Account>? Accounts { get; set; }

        [InverseProperty(nameof(Category.User))]
        [DeleteBehavior(DeleteBehavior.NoAction)]
        public ICollection<Category>? Categories { get; set; }

        [InverseProperty(nameof(Transaction.User))]
        [DeleteBehavior(DeleteBehavior.NoAction)]
        public ICollection<Transaction>? Transactions { get; set; }
    }
}
