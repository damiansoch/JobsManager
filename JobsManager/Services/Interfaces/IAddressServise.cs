using JobsManager.Dtos;
using JobsManager.Models;

namespace JobsManager.Services.Interfaces
{
    public interface IAddressServise
    {
        Task<IEnumerable<Address>> GetAllAsync();
        Task<int?> DeleteAsync(Guid id);
        Task<int?> CreateAsync(Guid customerId,AddAddressRequestDto addAddressRequestDto);
        Task<int?> UpdateAsync( Guid id,UpdateAddressRequestDto updateAddressRequestDto);
    }
}
