using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Application.Dtos.Accounts
{
    public class UpsertAccountDto
    {
        public Guid? UserId { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 3)]
        public string Name { get; set; } = null!;

        [Required]
        [Precision(18, 2)]
        public decimal Balance { get; set; } = 0.0m;

        [Required]
        public string Currency { get; set; } = "PHP";
    }
}
