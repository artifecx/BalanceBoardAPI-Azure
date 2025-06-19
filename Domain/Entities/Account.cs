using Domain.Abstractions;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public sealed class Account : BaseEntity
    {
        [Required]
        public Guid UserId { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 3)]
        public string Name { get; set; } = null!; //  "BDO Savings"

        [Required]
        [Precision(18,2)]
        public decimal Balance { get; set; } = 0.0m;

        [Required]
        public string Currency { get; set; } = "PHP"; // Default currency

        [Required]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;


        [InverseProperty(nameof(Transaction.Account))]
        public ICollection<Transaction>? Transactions { get; set; }

        [ForeignKey(nameof(UserId))]
        [InverseProperty(nameof(User.Accounts))]
        [DeleteBehavior(DeleteBehavior.NoAction)]
        public User User { get; set; } = null!;
    }
}
