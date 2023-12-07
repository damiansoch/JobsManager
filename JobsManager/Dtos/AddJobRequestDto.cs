using System.ComponentModel.DataAnnotations;

namespace JobsManager.Dtos
{
    public class AddJobRequestDto
    {
        [Required]
        [MaxLength(255)]
        public string Name { get; set; } = string.Empty;
        [Required]
        public string Description { get; set; } = string.Empty;
        [Required]
        public decimal Price { get; set; }
        [Required]
        public decimal Deposit { get; set; }
        [Required]
        public DateTime ToBeCompleted { get; set; }
    }
}
