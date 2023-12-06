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
                                  FROM [dbo].[Customers]";

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
    }
}
