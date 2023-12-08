using JobsManager.Dtos;
using JobsManager.Models;

namespace JobsManager.Services.Interfaces
{
    public interface ICustomerService
    {
        Task<IEnumerable<Customer>> GetAllAsync();
        Task<Customer?> GetCustomerAsync(Guid customerId);
        Task<int?> Delete(Guid customerId);
        Task<Tuple<Guid?,Customer>?> CreateAsync(AddCustomerRequestDto addCustomerRequestDto);
        Task<int?> UpdateAsync(Guid customerId,UpdateCustomerRequestDto updateCustomerRequestDto);
    }
}
