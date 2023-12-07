using System.ComponentModel.DataAnnotations;

namespace JobsManager.Dtos
{
    public class AddAddressRequestDto
    {
        public int? HouseNumber { get; set; }
        [MaxLength(255)]
        public string? AddressLine1 { get; set; }
        [MaxLength(255)]
        public string? AddressLine2 { get; set; } 
        [MaxLength(255)]
        public string? AddressLine3 { get; set; } 
        [MaxLength(50)]
        public string? PostCode { get; set; } 
    }

    
}
