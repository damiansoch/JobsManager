using System.ComponentModel.DataAnnotations;

namespace JobsManager.Dtos
{
    public class UpdateContactRequestDto
    {
        [Required]
        [MaxLength(50)]
        public string PhoneNumber { get; set; } = string.Empty;
        [MaxLength(50)]
        public string? PhoneNumber2 { get; set; }
        [Required]
        [MaxLength(255)]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;
        public string? ExtraDetails { get; set; }
    }
}
