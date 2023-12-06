using JobsManager.Models;

namespace JobsManager.Services.Interfaces
{
    public interface ICustomerService
    {
        Task<IEnumerable<Customer>> GetAllAsync();
    }
}
