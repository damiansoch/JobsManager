namespace JobsManager.Models
{
    public class Address
    {
        public Guid Id { get; set; }
        public Guid CustomerId { get; set; }
        public int HouseNumber { get; set; }
        public string AddressLine1 { get; set; }= string.Empty;
        public string AddressLine2 { get; set; }= string.Empty;
        public string AddressLine3 { get; set; }= string.Empty;
        public string PostCode { get; set;}= string.Empty;
    }
}
