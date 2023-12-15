using JobsManager.Models;
using JobsManager.Repositories.Interfaces;
using System.Data.SqlClient;
using Dapper;
using JobsManager.Dtos;

namespace JobsManager.Repositories
{
    public class ContactRepository:IContactRepository
    {
        private readonly string _connectionString;
        public ContactRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection")!;
        }


        public async Task<IEnumerable<Contact>> GetAllAsync()
        {
            const string query = @"SELECT [Id]
                                      ,[CustomerId]
                                      ,[PhoneNumber]
                                      ,[PhoneNumber2]
                                      ,[Email]
                                      ,[ExtraDetails]
                                  FROM [dbo].[Contacts]";
            try
            {
                await using var connection = new SqlConnection(_connectionString);
                var result = await connection.QueryAsync<Contact>(query);
                return result;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public async Task<int> CreateAsync(Contact contact)
        {
            const string query = @"INSERT INTO [dbo].[Contacts]
                                       ([Id]
                                       ,[CustomerId]
                                       ,[PhoneNumber]
                                       ,[PhoneNumber2]
                                       ,[Email]
                                       ,[ExtraDetails])
                                 VALUES
                                       (@Id
                                       ,@CustomerId
                                       ,@PhoneNumber
                                       ,@PhoneNumber2
                                       ,@Email
                                       ,@ExtraDetails
                                )";
            try
            {
                await using var connection = new SqlConnection(_connectionString);
                var result = await connection.ExecuteAsync(query,new
                {
                    contact.Id,
                    contact.CustomerId,
                    contact.PhoneNumber,
                    contact.PhoneNumber2,
                    contact.Email,
                    contact.ExtraDetails
                });
                return result;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public async Task<int> UpdateAsync(Contact contact)
        {
            const string query = @"UPDATE [dbo].[Contacts]
                                       SET [Id] = @Id
                                          ,[CustomerId] = @CustomerId
                                          ,[PhoneNumber] = @PhoneNumber
                                          ,[PhoneNumber2] = @PhoneNumber2
                                          ,[Email] = @Email
                                          ,[ExtraDetails] = @ExtraDetails
                                     WHERE Id = @Id";
            try
            {
                await using var connection = new SqlConnection(_connectionString);
                var result = await connection.ExecuteAsync(query, new
                {
                    contact.Id,
                    contact.CustomerId,
                    contact.PhoneNumber,
                    contact.PhoneNumber2,
                    contact.Email,
                    contact.ExtraDetails
                });
                return result;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public async Task<Contact?> GetByIdAsync(Guid id)
        {
            const string query = @"SELECT [Id]
                                      ,[CustomerId]
                                      ,[PhoneNumber]
                                      ,[PhoneNumber2]
                                      ,[Email]
                                      ,[ExtraDetails]
                                  FROM [dbo].[Contacts]
                                  WHERE Id = @Id";
            try
            {
                await using var connection = new SqlConnection(_connectionString);
                var result = await connection.QueryFirstOrDefaultAsync<Contact>(query,new {Id = id});
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
