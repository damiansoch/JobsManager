using System.ComponentModel.DataAnnotations;
using JobsManager.Models;

namespace JobsManager.Dtos
{
    public class AddCustomerRequestDto
    {
        [Required]
        [MaxLength(255)]
        public string FirstName { get; set; } = string.Empty;
        [Required]
        [MaxLength(255)]
        public string LastName { get; set; } = string.Empty;
        [Required]
        [MaxLength(255)]
        public string CompanyName { get; set; } = string.Empty;
        [Required]
        [MaxLength(50)]
        public string PhoneNumber { get; set; } = string.Empty;
        [MaxLength(50)]
        public string? PhoneNumber2 { get; set; }
        [Required]
        [MaxLength(200)]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;
        public string? ExtraDetails { get; set; }
    }
}
