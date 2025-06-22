namespace Application.Dtos.Auth;

public class RegisterUserDto
{
    public string Email { get; set; } = null!;
    public string Password { get; set; } = null!;
    public string? ConfirmPassword { get; set; } = null!;
}
