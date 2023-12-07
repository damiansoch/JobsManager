namespace JobsManager.Models
{
    public class Contact
    {
        public Guid Id { get; set; }
        public Guid CustomerId { get; set; }
        public string PhoneNumber { get; set; }=string.Empty;
        public string? PhoneNumber2 { get; set; }
        public string Email { get; set; }=string.Empty;
        public string? ExtraDetails { get; set; }
        

    }
}
