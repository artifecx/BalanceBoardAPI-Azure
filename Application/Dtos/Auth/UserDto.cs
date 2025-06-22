namespace Application.Dtos.Auth;

public sealed record UserDto
(
    string Username,
    string Email,
    DateTime CreatedAt
);