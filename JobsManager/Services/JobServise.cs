using JobsManager.Dtos;
using JobsManager.Models;
using JobsManager.Repositories.Interfaces;
using JobsManager.Services.Interfaces;

namespace JobsManager.Services
{
    public class JobServise : IJobServise
    {
        private readonly IJobRepository _jobRepository;
        private readonly ICustomerService _customerService;

        public JobServise(IJobRepository jobRepository, ICustomerService customerService)
        {
            _jobRepository = jobRepository;
            _customerService = customerService;
        }

        public async Task<IEnumerable<Job>> GetAllAsync()
        {
            var result = await _jobRepository.GetAllAsync();
            return result;
        }

        public async Task<IEnumerable<Job>?> GetAllJobsForCustomerAsync(Guid customerId)
        {
            var existingCustomer = await _customerService.GetCustomerAsync(customerId);
            if (existingCustomer is null)
                return null;

            var result = await _jobRepository.GetAllJobsForCustomerAsync(customerId);
            return result;
        }

        public async Task<int?> CreateJobAsync(Guid customerId, AddJobRequestDto addJobRequestDto)
        {
            var existingCustomer = await _customerService.GetCustomerAsync(customerId);
            if (existingCustomer is null)
                return null;

            var job = new Job
            {
                Id = Guid.NewGuid(),
                CustomerId = customerId,
                Name = addJobRequestDto.Name,
                Description = addJobRequestDto.Description,
                Price = addJobRequestDto.Price,
                Deposit = addJobRequestDto.Deposit,
                Created = DateTime.Now,
                ToBeCompleted = addJobRequestDto.ToBeCompleted,
            };

            var response = await _jobRepository.CreateJobAsync(job);
            return response;
        }

        public async Task<int?> UpdateJobAsync(Guid id, UpdateJobRequestDto updateJobRequestDto)
        {
            var allJobs = await GetAllAsync();
            var existingJob = allJobs.FirstOrDefault(x => x.Id == id);
            if(existingJob is null)
                return null;

            existingJob.Name = updateJobRequestDto.Name;
            existingJob.Description = updateJobRequestDto.Description;
            existingJob.Price = updateJobRequestDto.Price;
            existingJob.Deposit = updateJobRequestDto.Deposit;
            existingJob.ToBeCompleted = updateJobRequestDto.ToBeCompleted;

            var response = await _jobRepository.UpdateJobAsync(existingJob);
            return response;
        }

        public async Task<int?> DeleteJobAsync(Guid id)
        {
            var allJobs = await GetAllAsync();
            var existingJob = allJobs.FirstOrDefault(x => x.Id == id);
            if (existingJob is null)
                return null;

            var response = await _jobRepository.DeleteJobAsync(id);
            return response;
        }
    }
}
