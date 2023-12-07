﻿using JobsManager.Models;

namespace JobsManager.Repositories.Interfaces
{
    public interface IJobRepository
    {
        Task<IEnumerable<Job>> GetAllAsync();
        Task<IEnumerable<Job>> GetAllJobsForCustomerAsync(Guid customerId);
        Task<int>CreateJobAsync(Job job);
        Task<int>UpdateJobAsync(Job job);
        Task<int>DeleteJobAsync(Guid id);
    }
}
