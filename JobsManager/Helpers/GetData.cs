using JobsManager.Models;
using JobsManager.Repositories.Interfaces;

namespace JobsManager.Helpers
{
    public class GetData:IGetData
    {
        private readonly IJobRepository _jobRepository;
        private readonly IAddressRepository _addressRepository;

        public GetData(IJobRepository jobRepository,IAddressRepository addressRepository)
        {
            _jobRepository = jobRepository;
            _addressRepository = addressRepository;
        }
        public async Task<IEnumerable<Job>> GetAllJobsForCustomer(Guid customerId)
        {
            var result = await _jobRepository.GetAllJobsForCustomerAsync(customerId);
            return result;
        }

        public async Task<IEnumerable<Address>> GetAllAddressesForCustomer(Guid customerId)
        {

            var results = await _addressRepository.GetAllForCustomer(customerId);
            return results;
        }
    }
}
