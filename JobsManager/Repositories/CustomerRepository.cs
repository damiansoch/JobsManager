using System.Data.SqlClient;
using Dapper;
using JobsManager.Models;
using JobsManager.Repositories.Interfaces;

namespace JobsManager.Repositories
{
    public class CustomerRepository:ICustomerRepository
    {
        private readonly string _connectionString;

        public CustomerRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection")!;
        }
        public async Task<IEnumerable<Customer>> GetAllAsync()
        {
            const string query = @"SELECT [Id]
                                      ,[FirstName]
                                      ,[LastName]
                                      ,[CompanyName]
                                      ,[Created]
                                  FROM [dbo].[Customers] 
                                  ORDER BY [Created] desc";

            try
            {
                await using var connection = new SqlConnection(_connectionString);
                var result = await connection.QueryAsync<Customer>(query);
                return result;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public async Task<Customer?> GetCustomerAsync(Guid customerId)
        {
            const string query = @"SELECT [Id]
                                      ,[FirstName]
                                      ,[LastName]
                                      ,[CompanyName]
                                      ,[Created]
                                  FROM [dbo].[Customers] 
                                  WHERE Id = @Id";

            try
            {
                await using var connection = new SqlConnection(_connectionString);
                var result = await connection.QueryFirstOrDefaultAsync<Customer>(query,new {Id= customerId});
                return result;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public async Task<int> DeleteAsync(Guid customerId)
        {
            const string query = @"DELETE FROM [dbo].[Customers]
                                    WHERE Id = @Id";

            try
            {
                await using var connection = new SqlConnection(_connectionString);
                var result = await connection.ExecuteAsync(query, new { Id = customerId });
                return result;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public async Task<Guid?> CreateAsync( Customer customer)
        {
            const string query = @"INSERT INTO [dbo].[Customers]
                                       ([Id]
                                       ,[FirstName]
                                       ,[LastName]
                                       ,[CompanyName]
                                      )
                                 VALUES
                                       (@Id
                                       ,@FirstName
                                       ,@LastName
                                       ,@CompanyName
                                       )";

            try
            {
                await using var connection = new SqlConnection(_connectionString);
                var result = await connection.ExecuteAsync(query, new
                {
                    customer.Id, 
                    customer.FirstName , 
                    customer.LastName,
                    customer.CompanyName
                });

                if (result < 1)
                    return null;
                return customer.Id;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public async Task<int> UpdateAsync(Customer customer)
        {
            const string query = @"UPDATE [dbo].[Customers]
                                   SET [Id] = @Id
                                      ,[FirstName] = @FirstName
                                      ,[LastName] = @LastName
                                      ,[CompanyName] = @CompanyName
                                 WHERE Id=@Id";

            try
            {
                await using var connection = new SqlConnection(_connectionString);
                var result = await connection.ExecuteAsync(query, new
                {
                    customer.Id,
                    customer.FirstName,
                    customer.LastName,
                    customer.CompanyName
                });

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
