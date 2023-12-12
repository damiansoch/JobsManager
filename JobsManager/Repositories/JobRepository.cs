using JobsManager.Models;
using JobsManager.Repositories.Interfaces;
using System.Data.SqlClient;
using Dapper;

namespace JobsManager.Repositories
{
    public class JobRepository : IJobRepository
    {
        private readonly string _connectionString;
        public JobRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection")!;
        }

        public async Task<IEnumerable<Job>> GetAllAsync()
        {
            const string query = @"SELECT [Id]
                                      ,[CustomerId]
                                      ,[Name]
                                      ,[Description]
                                      ,[Price]
                                      ,[Deposit]
                                      ,[Balance]
                                      ,[Created]
                                      ,[ToBeCompleted]
                                  FROM [dbo].[Jobs]
                                  ORDER BY [ToBeCompleted] desc";
            try
            {
                await using var connection = new SqlConnection(_connectionString);
                var result = await connection.QueryAsync<Job>(query);
                return result;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public async Task<IEnumerable<Job>> GetAllJobsForCustomerAsync(Guid customerId)
        {
            const string query = @"SELECT [Id]
                                      ,[CustomerId]
                                      ,[Name]
                                      ,[Description]
                                      ,[Price]
                                      ,[Deposit]
                                      ,[Balance]
                                      ,[Created]
                                      ,[ToBeCompleted]
                                  FROM [dbo].[Jobs] 
                                  WHERE CustomerId = @CustomerId";
            try
            {
                await using var connection = new SqlConnection(_connectionString);
                var result = await connection.QueryAsync<Job>(query,new { CustomerId = customerId });
                return result;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public async Task<int> CreateJobAsync(Job job)
        {
            const string query = @"INSERT INTO [dbo].[Jobs]
                                       ([Id]
                                       ,[CustomerId]
                                       ,[Name]
                                       ,[Description]
                                       ,[Price]
                                       ,[Deposit]
                                       ,[Created]
                                       ,[ToBeCompleted])
                                 VALUES
                                       (@Id
                                       ,@CustomerId
                                       ,@Name
                                       ,@Description
                                       ,@Price
                                       ,@Deposit
                                       ,@Created
                                       ,@ToBeCompleted
                                 )";
            try
            {
                await using var connection = new SqlConnection(_connectionString);
                var result = await connection.ExecuteAsync(query, new
                {
                    job.Id,
                    job.CustomerId,
                    job.Name,
                    job.Description,
                    job.Price,
                    job.Deposit,
                    job.Created,
                    job.ToBeCompleted

                });
                return result;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public async Task<int> UpdateJobAsync(Job job)
        {
            const string query = @"UPDATE [dbo].[Jobs]
                                       SET [Name] = @Name
                                          ,[Description] = @Description
                                          ,[Price] = @Price
                                          ,[Deposit] = @Deposit                                        
                                          ,[ToBeCompleted] = @ToBeCompleted
                                     WHERE Id = @Id";
            try
            {
                await using var connection = new SqlConnection(_connectionString);
                var result = await connection.ExecuteAsync(query, new
                {
                    job.Id,
                    job.Name,
                    job.Description,
                    job.Price,
                    job.Deposit,
                    job.ToBeCompleted

                });
                return result;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public async Task<int> DeleteJobAsync(Guid id)
        {
            const string query = @"DELETE FROM [dbo].[Jobs]
                                    WHERE Id = @Id";
            try
            {
                await using var connection = new SqlConnection(_connectionString);
                var result = await connection.ExecuteAsync(query, new { Id = id });
                return result;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public async Task<Job?> GetByIdAsync(Guid id)
        {
            const string query = @"SELECT [Id]
                                  ,[CustomerId]
                                  ,[Name]
                                  ,[Description]
                                  ,[Price]
                                  ,[Deposit]
                                  ,[Balance]
                                  ,[Created]
                                  ,[ToBeCompleted]
                              FROM [dbo].[Jobs]
                              WHERE Id = @Id";
            try
            {
                await using var connection = new SqlConnection(_connectionString);
                var result = await connection.QueryFirstOrDefaultAsync<Job>(query, new { Id = id });
                return result;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}
