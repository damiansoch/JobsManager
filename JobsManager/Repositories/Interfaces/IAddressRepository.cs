using JobsManager.Models;

namespace JobsManager.Repositories.Interfaces
{
    public interface IAddressRepository
    {
        Task<IEnumerable<Address>> GetAllAsync();
        Task<IEnumerable<Address>> GetAllForCustomer(Guid customerId);
        Task<int> DeleteAsync(Guid id);
        Task<int> CreateAsync(Address address);
        Task<int> UpdateAsync(Address address);
        Task<Address?>GetById(Guid id);
    }
}
