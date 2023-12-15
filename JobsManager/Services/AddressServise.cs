using JobsManager.Dtos;
using JobsManager.Models;
using JobsManager.Repositories.Interfaces;
using JobsManager.Services.Interfaces;

namespace JobsManager.Services
{
    public class AddressServise : IAddressServise
    {
        private readonly IAddressRepository _addressRepository;
        private readonly ICustomerService _customerService;

        public AddressServise(IAddressRepository addressRepository,ICustomerService customerService)
        {
            _addressRepository = addressRepository;
            _customerService = customerService;
        }

        public async Task<IEnumerable<Address>> GetAllAsync()
        {
            var result = await _addressRepository.GetAllAsync();
            return result;
        }

        public async Task<int?> DeleteAsync(Guid id)
        {
            var existingAddress = await DoesAddressExist(id);
            if (existingAddress is null)
                return null;

            var response = await _addressRepository.DeleteAsync(id);
            return response;
        }

        public async Task<int?> CreateAsync(Guid customerId, AddAddressRequestDto addAddressRequestDto)
        {
            var existingCustomer = await _customerService.GetCustomerAsync(customerId);
            if (existingCustomer is null)
                return null;

            var address = new Address
            {
                Id = Guid.NewGuid(),
                CustomerId = customerId,
                HouseNumber = addAddressRequestDto.HouseNumber,
                AddressLine1 = addAddressRequestDto.AddressLine1,
                AddressLine2 = addAddressRequestDto.AddressLine2,
                AddressLine3 = addAddressRequestDto.AddressLine3,
                PostCode = addAddressRequestDto.PostCode,
            };

            var result = await _addressRepository.CreateAsync(address);
            return result;
        }

        public async Task<int?> UpdateAsync(Guid id, UpdateAddressRequestDto updateAddressRequestDto)
        {
            var existingAddress = await DoesAddressExist(id);
            if (existingAddress is null)
                return null;

            existingAddress.HouseNumber = updateAddressRequestDto.HouseNumber;
            existingAddress.AddressLine1 = updateAddressRequestDto.AddressLine1;
            existingAddress.AddressLine2 = updateAddressRequestDto.AddressLine2;
            existingAddress.AddressLine3 = updateAddressRequestDto.AddressLine3;
            existingAddress.PostCode = updateAddressRequestDto.PostCode;

            var response = await _addressRepository.UpdateAsync(existingAddress);
            return response;
        }

        public async Task<Address?> GetById(Guid id)
        {
            var response = await _addressRepository.GetById(id);
            return response;
        }

        private async Task<Address?> DoesAddressExist(Guid id)
        {
            var allAddresses = await GetAllAsync();
            var existingAddress = allAddresses.FirstOrDefault(x => x.Id == id);
            return existingAddress ?? null;
        }
    }
}
