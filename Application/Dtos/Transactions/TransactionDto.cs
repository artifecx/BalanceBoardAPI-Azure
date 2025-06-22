using Domain.Entities;

namespace Application.Dtos.Transactions;

public sealed record TransactionDto
(
    Guid Id,
    Guid AccountId,
    Guid CategoryId,
    decimal Amount,
    string? Note,
    DateTime TransactionDate,
    TransactionType Type
);
