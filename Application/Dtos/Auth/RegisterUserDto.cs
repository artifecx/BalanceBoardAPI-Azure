using System.ComponentModel.DataAnnotations;

namespace Application.Dtos;

public class RegisterUserDto
{

    [Required]
    [EmailAddress]
    public string Email { get; set; } = null!;

    [Required]
    [MinLength(6)]
    [DataType(DataType.Password)]
    public string Password { get; set; } = null!;
    
    [Required]
    [Compare(nameof(Password), ErrorMessage = "Confirmation password does not match.")]
    public string? ConfirmPassword { get; set; } = null!;
}
