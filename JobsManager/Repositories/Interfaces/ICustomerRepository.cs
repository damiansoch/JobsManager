using JobsManager.Models;

namespace JobsManager.Repositories.Interfaces
{
    public interface ICustomerRepository
    {
        Task<IEnumerable<Customer>> GetAllAsync();
    }
}
