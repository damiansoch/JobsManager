using JobsManager.Models;
using JobsManager.Repositories.Interfaces;
using System.Data.SqlClient;
using Dapper;

namespace JobsManager.Repositories
{
    public class AddressRepository:IAddressRepository
    {
        private readonly string _connectionString;
        public AddressRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection")!;
        }

        public async Task<IEnumerable<Address>> GetAllAsync()
        {
            const string query = @"SELECT [Id]
                                      ,[CustomerId]
                                      ,[HouseNumber]
                                      ,[AddressLine1]
                                      ,[AddressLine2]
                                      ,[AddressLine3]
                                      ,[PostCode]
                                  FROM [dbo].[Addresses]";
            try
            {
                await using var connection = new SqlConnection(_connectionString);
                var result = await connection.QueryAsync<Address>(query);
                return result;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public async Task<IEnumerable<Address>> GetAllForCustomer(Guid customerId)
        {
            const string query = @"SELECT [Id]
                                      ,[CustomerId]
                                      ,[HouseNumber]
                                      ,[AddressLine1]
                                      ,[AddressLine2]
                                      ,[AddressLine3]
                                      ,[PostCode]
                                  FROM [dbo].[Addresses] 
                                  WHERE CustomerId = @CustomerId;";
            try
            {
                await using var connection = new SqlConnection(_connectionString);
                var result = await connection.QueryAsync<Address>(query,new { CustomerId = customerId });
                return result;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public async Task<int> DeleteAsync(Guid id)
        {
            const string query = @"DELETE FROM [dbo].[Addresses]
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

        public async Task<int> CreateAsync(Address address)
        {
            const string query = @"INSERT INTO [dbo].[Addresses]
                                       ([Id]
                                       ,[CustomerId]
                                       ,[HouseNumber]
                                       ,[AddressLine1]
                                       ,[AddressLine2]
                                       ,[AddressLine3]
                                       ,[PostCode])
                                 VALUES
                                       (@Id
                                       ,@CustomerId
                                       ,@HouseNumber
                                       ,@AddressLine1
                                       ,@AddressLine2
                                       ,@AddressLine3
                                       ,@PostCode
                                )";
            try
            {
                await using var connection = new SqlConnection(_connectionString);
                var result = await connection.ExecuteAsync(query, new
                {
                    address.Id,
                    address.CustomerId,
                    address.HouseNumber,
                    address.AddressLine1,
                    address.AddressLine2,
                    address.AddressLine3,
                    address.PostCode
                });
                return result;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public async Task<int> UpdateAsync(Address address)
        {
            const string query = @"UPDATE [dbo].[Addresses]
                                   SET [HouseNumber] = @HouseNumber
                                      ,[AddressLine1] = @AddressLine1
                                      ,[AddressLine2] = @AddressLine2
                                      ,[AddressLine3] = @AddressLine3
                                      ,[PostCode] = @PostCode
                                 WHERE Id = @Id";
            try
            {
                await using var connection = new SqlConnection(_connectionString);
                var result = await connection.ExecuteAsync(query, new
                {
                    address.Id,
                    address.HouseNumber,
                    address.AddressLine1,
                    address.AddressLine2,
                    address.AddressLine3,
                    address.PostCode
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
