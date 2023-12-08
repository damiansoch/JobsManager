using JobsManager.Models;

namespace JobsManager.Helpers
{
    public interface IGetData
    {
        Task<IEnumerable<Job>>GetAllJobsForCustomer(Guid customerId);
        Task<IEnumerable<Address>> GetAllAddressesForCustomer(Guid customerId);
    }
}
