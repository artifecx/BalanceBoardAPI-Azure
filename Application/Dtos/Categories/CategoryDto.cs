using Application.Dtos.Transactions;
using Domain.Entities;

namespace Application.Dtos.Categories;

public sealed record CategoryDto
(
    Guid Id,
    string Name,
    TransactionType Type,
    DateTime CreatedAt,
    List<TransactionDto>? Transactions
);
