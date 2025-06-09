using Microsoft.Data.SqlClient;
using Microsoft.Identity.Client;
using OnlinePaymentsApp.Models;
using OnlinePaymentsApp.Repository.Base;
using OnlinePaymentsApp.Repository.Helpers;
using OnlinePaymentsApp.Repository.Interfaces.Account;
using System.Reflection.PortableExecutable;

namespace OnlinePaymentsApp.Repository.Implementations.Account
{
    public class AccountRepository : BaseRepository<Models.Account>, IAccountRepository
    {
        private const string IdDbFieldEnumeratorName = "AccountId";

        protected override string GetTableName()
        {
            return "Accounts";
        }

        protected override string[] GetColumns() => new[]
        {
            IdDbFieldEnumeratorName,
            "AccountNumber",
            "Balance"
        };

        protected override Models.Account MapEntity(SqlDataReader reader)
        {
            return new Models.Account
            {
                AccountId = Convert.ToInt32(reader[IdDbFieldEnumeratorName]),
                AccountNumber = Convert.ToString(reader["AccountNumber"]),
                Balance = Convert.ToDecimal(reader["Balance"])
            };
        }

        public Task<int> CreateAsync(Models.Account entity)
        {
            return base.CreateAsync(entity, IdDbFieldEnumeratorName);
        }

        public Task<Models.Account> RetrieveAsync(int objectId)
        {
            return base.RetrieveAsync(IdDbFieldEnumeratorName, objectId);
        }

        public IAsyncEnumerable<Models.Account> RetrieveCollectionAsync(AccountFilter filter)
        {
            Filter commandFilter = new Filter();

            if (filter.AccountNumber is not null)
            {
                commandFilter.AddCondition("AccountNumber", filter.AccountNumber);
            }

            return base.RetrieveCollectionAsync(commandFilter);
        }

        public async Task<bool> UpdateAsync(int objectId, AccountUpdate update)
        {
            using SqlConnection connection = await ConnectionFactory.CreateConnectionAsync();

            UpdateCommand updateCommand = new UpdateCommand(connection, "Accounts", IdDbFieldEnumeratorName, objectId);

            updateCommand.AddSetClause("Balance", update.Balance);

            return await updateCommand.ExecuteNonQueryAsync() > 0;
        }

        public Task<bool> DeleteAsync(int objectId)
        {
            return base.DeleteAsync(IdDbFieldEnumeratorName, objectId);
        }

        public async Task<Models.Account> GetByAccountNumberAsync(string accountNumber)
        {
            using SqlConnection connection = await ConnectionFactory.CreateConnectionAsync();
            using SqlCommand sqlCommand = connection.CreateCommand();
            using SqlTransaction transaction = connection.BeginTransaction();

            sqlCommand.Transaction = transaction;
            sqlCommand.CommandText = "SELECT * FROM Accounts WHERE AccountNumber = @AccountNumber";
            sqlCommand.Parameters.AddWithValue("@AccountNumber", accountNumber);
            using SqlDataReader reader = await sqlCommand.ExecuteReaderAsync();
            if (reader.Read())
            {
                var result = MapEntity(reader);

                if (reader.Read())
                {
                    throw new Exception("Multiple records found for the same ID.");
                }

                return result;
            }
            else
            {
                return default(Models.Account);
            }
        }
    }
}
