using JobsManager.Models;
using JobsManager.Repositories.Interfaces;
using JobsManager.Services.Interfaces;

namespace JobsManager.Services
{
    public class CustomerService:ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;

        public CustomerService(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }
        public async Task<IEnumerable<Customer>> GetAllAsync()
        {
            var result = await _customerRepository.GetAllAsync();
            return result;
        }
    }
}
