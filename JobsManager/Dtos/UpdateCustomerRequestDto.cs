using System.ComponentModel.DataAnnotations;

namespace JobsManager.Dtos
{
    public class UpdateCustomerRequestDto
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
      
    }
}
