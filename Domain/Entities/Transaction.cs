using Domain.Abstractions;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public sealed class Transaction : BaseEntity
    {
        [Required]
        public Guid UserId { get; set; }

        [Required]
        public Guid AccountId { get; set; }

        [Required]
        public Guid CategoryId { get; set; }

        [Required]
        [Precision(18, 2)]
        public decimal Amount { get; set; } = 0.0m;

        public string? Note { get; set; } = string.Empty;

        [Required]
        public DateTime TransactionDate { get; set; } = DateTime.UtcNow;

        [Required]
        public TransactionType Type { get; set; } = TransactionType.Expense;


        [ForeignKey(nameof(AccountId))]
        [InverseProperty(nameof(Account.Transactions))]
        [DeleteBehavior(DeleteBehavior.Cascade)]
        public Account Account { get; set; } = null!;

        [ForeignKey(nameof(CategoryId))]
        [InverseProperty(nameof(Category.Transactions))]
        [DeleteBehavior(DeleteBehavior.NoAction)]
        public Category Category { get; set; } = null!;

        [ForeignKey(nameof(UserId))]
        [InverseProperty(nameof(User.Transactions))]
        [DeleteBehavior(DeleteBehavior.NoAction)]
        public User User { get; set; } = null!;
    }

    public enum TransactionType
    {
        Expense,
        Income
    }
}
