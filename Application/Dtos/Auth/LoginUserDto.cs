using System.ComponentModel.DataAnnotations;

namespace Application.Dtos;

public class LoginUserDto
{
    [Required]
    [EmailAddress]
    public string Email { get; set; } = null!;

    [Required]
    [DataType(DataType.Password)]
    public string Password { get; set; } = null!;
}