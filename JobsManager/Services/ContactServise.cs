using System.Runtime.InteropServices.ComTypes;
using JobsManager.Dtos;
using JobsManager.Models;
using JobsManager.Repositories.Interfaces;
using JobsManager.Services.Interfaces;

namespace JobsManager.Services
{
    public class ContactServise:IContactServise
    {
        private readonly IContactRepository _contactRepository;

        public ContactServise(IContactRepository contactRepository)
        {
            _contactRepository = contactRepository;
        }
        public async Task<IEnumerable<Contact>> GetAllAsync()
        {
            var result = await _contactRepository.GetAllAsync();
            return result;
        }

        public async Task<int> CreateAsync(Contact contact)
        {
            var result = await _contactRepository.CreateAsync(contact);
            return result;
        }

        public async Task<int?> UpdateAsync(Guid contactId, UpdateContactRequestDto updateContactRequestDto)
        {
            var allContacts = await GetAllAsync();
            var existingContact = allContacts.FirstOrDefault(x=>x.Id == contactId);
            if (existingContact is null) return null;

            existingContact.PhoneNumber = updateContactRequestDto.PhoneNumber;
            existingContact.PhoneNumber2 = updateContactRequestDto.PhoneNumber2;
            existingContact.Email = updateContactRequestDto.Email;
            existingContact.ExtraDetails = updateContactRequestDto.ExtraDetails;

            var result = await _contactRepository.UpdateAsync(existingContact);
            return result;
        }
    }
}
