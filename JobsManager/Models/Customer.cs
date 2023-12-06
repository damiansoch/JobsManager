namespace JobsManager.Models
{
    public class Customer
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }=string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string CompanyName { get; set; } = string.Empty;
        public DateTime Created { get; set; }

        public Contact Contact { get; set; } = new Contact();
        public List<Job> Jobs { get; set; } = new List<Job>();
        public List<Address> Addresses { get; set; } = new List<Address>();


    }
}
