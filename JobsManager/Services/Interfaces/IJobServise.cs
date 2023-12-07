using JobsManager.Dtos;
using JobsManager.Models;

namespace JobsManager.Services.Interfaces
{
    public interface IJobServise
    {
        Task<IEnumerable<Job>> GetAllAsync();
        Task<IEnumerable<Job>?> GetAllJobsForCustomerAsync(Guid customerId);
        Task<int?> CreateJobAsync(Guid customerId,AddJobRequestDto addJobRequestDto);
        Task<int?> UpdateJobAsync(Guid id,UpdateJobRequestDto updateJobRequestDto);
        Task<int?> DeleteJobAsync(Guid id);
    }
}
