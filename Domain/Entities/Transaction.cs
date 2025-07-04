﻿using Domain.Abstractions;

namespace Domain.Entities
{
    public sealed class Transaction : BaseEntity
    {
        public Guid UserId { get; set; }
        public Guid AccountId { get; set; }
        public Guid? CategoryId { get; set; }
        public decimal Amount { get; set; } = 0.0m;
        public string? Note { get; set; } = string.Empty;
        public DateTime TransactionDate { get; set; } = DateTime.UtcNow;
        public TransactionType Type { get; set; } = TransactionType.Expense;

        public Account Account { get; private set; } = null!;
        public Category? Category { get; private set; }
        public User User { get; private set; } = null!;
    }

    public enum TransactionType
    {
        Expense,
        Income
    }
}
