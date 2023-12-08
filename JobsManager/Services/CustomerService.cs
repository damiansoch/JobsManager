using System.Transactions;
using JobsManager.Dtos;
using JobsManager.Helpers;
using JobsManager.Models;
using JobsManager.Repositories.Interfaces;
using JobsManager.Services.Interfaces;

namespace JobsManager.Services
{
    public class CustomerService:ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IContactServise _contactServise;
        private readonly IGetData _getData;

        public CustomerService(ICustomerRepository customerRepository,IContactServise contactServise,IGetData getData)
        {
            _customerRepository = customerRepository;
            _contactServise = contactServise;
            _getData = getData;
        }
        public async Task<IEnumerable<Customer>> GetAllAsync()
        {
            var result = await _customerRepository.GetAllAsync();
            return result;
        }

        public async Task<Customer?> GetCustomerAsync(Guid customerId)
        {
            var result = await _customerRepository.GetCustomerAsync(customerId);

            if (result is null) return result;

            //getting the contact for the customer
            var allContacts = await _contactServise.GetAllAsync();
            var customerContact = allContacts.FirstOrDefault(x=>x.CustomerId==customerId);
            if (customerContact is not null) 
                result.Contact = customerContact;
            //getting all jobs for customer
            var jobs = await _getData.GetAllJobsForCustomer(customerId);
            result.Jobs = jobs.ToList();
            //getting all addresses for customer
            var addresses = await _getData.GetAllAddressesForCustomer(customerId);
            result.Addresses = addresses.ToList();
            return result;
        }

        public async Task<int?> Delete(Guid customerId)
        {
            var existingCustomer = await GetCustomerAsync(customerId);
            if (existingCustomer is null)
                return null;

            var response = await _customerRepository.DeleteAsync(customerId);
            return response;
        }

        public async Task<Tuple<Guid?, Customer>?> CreateAsync(AddCustomerRequestDto addCustomerRequestDto)
        {
            var allContacts = await _contactServise.GetAllAsync();
            var existingEmail = allContacts.FirstOrDefault(x =>
                string.Equals(x.Email, addCustomerRequestDto.Email, StringComparison.OrdinalIgnoreCase));
            if (existingEmail is not null)
                return null;
            var customer = new Customer
            {
                Id = Guid.NewGuid(),
                FirstName = addCustomerRequestDto.FirstName,
                LastName = addCustomerRequestDto.LastName,
                CompanyName = addCustomerRequestDto.CompanyName,
                Contact = new Contact(),
                Addresses = new List<Address>(),
                Jobs = new List<Job>()
            };
            var contact = new Contact
            {
                Id = Guid.NewGuid(),
                CustomerId = customer.Id,
                PhoneNumber = addCustomerRequestDto.PhoneNumber,
                PhoneNumber2 = addCustomerRequestDto.PhoneNumber2,
                Email = addCustomerRequestDto.Email,
                ExtraDetails = addCustomerRequestDto.ExtraDetails
            };

            
           
            using var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
            try
            {
                var customerResult = await _customerRepository.CreateAsync(customer);
                await _contactServise.CreateAsync(contact);
                //if both of them completed,commit the transaction
                scope.Complete();
                customer.Contact = contact;
                var resultTuple = new Tuple<Guid?, Customer>(customerResult, customer);
                return resultTuple;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public async Task<int?> UpdateAsync(Guid customerId, UpdateCustomerRequestDto updateCustomerRequestDto)
        {
            var existingCustomer = await GetCustomerAsync(customerId);
            if (existingCustomer is null)
                return null;

            existingCustomer.FirstName = updateCustomerRequestDto.FirstName;
            existingCustomer.LastName = updateCustomerRequestDto.LastName;
            existingCustomer.CompanyName = updateCustomerRequestDto.CompanyName;

            var result = await _customerRepository.UpdateAsync(existingCustomer);
            return result;
        }
    }
}
