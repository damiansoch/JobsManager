using JobsManager.Models;

namespace JobsManager.Repositories.Interfaces
{
    public interface ICustomerRepository
    {
        Task<IEnumerable<Customer>> GetAllAsync();
        Task<Customer?>GetCustomerAsync(Guid customerId);
        Task<int>DeleteAsync(Guid customerId);
        Task<Guid?> CreateAsync( Customer customer);
        Task<int> UpdateAsync ( Customer customer );
    }
}
