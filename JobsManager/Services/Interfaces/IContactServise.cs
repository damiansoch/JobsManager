using JobsManager.Dtos;
using JobsManager.Models;

namespace JobsManager.Services.Interfaces
{
    public interface IContactServise
    {
        Task<IEnumerable<Contact>> GetAllAsync();
        Task<int> CreateAsync(Contact contact);
        Task<int?> UpdateAsync(Guid contactId,UpdateContactRequestDto updateContactRequestDto);
        Task<Contact?> GetByIdAsync(Guid id);
    }
}
