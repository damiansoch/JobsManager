namespace JobsManager.Models
{
    public class Job
    {
        public Guid Id { get; set; }
        public Guid CustomerId { get; set; }
        public string Name { get; set; }=string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public decimal Deposit { get; set; }
        public decimal? Balance { get; set; } 
        public DateTime? Created { get; set; }
        public DateTime ToBeCompleted { get; set; }
        public bool Completed { get; set; }

    }
}
