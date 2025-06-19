using System.ComponentModel.DataAnnotations;

namespace Application.Dtos;

public sealed record UserDto
(
    string Username,
    string Email,
    DateTime CreatedAt
);