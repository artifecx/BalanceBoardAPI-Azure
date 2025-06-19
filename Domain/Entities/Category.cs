using Domain.Abstractions;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public sealed class Category : BaseEntity
    {
        [Required]
        public Guid UserId { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; } = null!; // "Groceries", "Salary"

        [Required]
        public TransactionType Type { get; set; } = TransactionType.Expense;

        [Required]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;


        [InverseProperty(nameof(Transaction.Category))]
        [DeleteBehavior(DeleteBehavior.NoAction)]
        public ICollection<Transaction>? Transactions { get; set; }

        [ForeignKey(nameof(UserId))]
        [InverseProperty(nameof(User.Categories))]
        [DeleteBehavior(DeleteBehavior.NoAction)]
        public User User { get; set; } = null!;
    }
}
