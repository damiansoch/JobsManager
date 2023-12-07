using JobsManager.Dtos;
using JobsManager.Models;

namespace JobsManager.Repositories.Interfaces
{
    public interface IContactRepository
    {
        Task<IEnumerable<Contact>> GetAllAsync();
        Task<int> CreateAsync( Contact contact);
        Task<int> UpdateAsync( Contact contact );
    }
}
