using System.ComponentModel.DataAnnotations;

namespace Application.Dtos.Auth
{
    public class RefreshTokenRequestDto
    {
        [Required]
        public Guid UserId { get; set; }

        [Required]
        public string RefreshToken { get; set; } = null!;
    }
}
