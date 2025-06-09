using Microsoft.Data.SqlClient;
using OnlinePaymentsApp.Repository.Base;
using OnlinePaymentsApp.Repository.Helpers;
using OnlinePaymentsApp.Repository.Interfaces.UserAccount;

namespace OnlinePaymentsApp.Repository.Implementations.UserAccount
{
    public class UserAccountRepository : BaseRepository<Models.UserAccount>, IUserAccountRepository
    {
        private const string IdDbFieldEnumeratorName = "UserId";

        protected override string GetTableName()
        {
            return "UserAccounts";
        }

        protected override string[] GetColumns() => new[]
        {
            IdDbFieldEnumeratorName,
            "AccountId"
        };

        protected override Models.UserAccount MapEntity(SqlDataReader reader)
        {
            return new Models.UserAccount
            {
                UserId = Convert.ToInt32(reader[IdDbFieldEnumeratorName]),
                AccountId = Convert.ToInt32(reader["AccountId"])
            };
        }

        public Task<int> CreateAsync(Models.UserAccount entity)
        {
            return base.CreateAsync(entity);
        }

        public Task<Models.UserAccount> RetrieveAsync(int objectId)
        {
            return base.RetrieveAsync(IdDbFieldEnumeratorName, objectId);
        }

        public IAsyncEnumerable<Models.UserAccount> RetrieveCollectionAsync(UserAccountFilter filter)
        {
            Filter commandFilter = new Filter();

            if (filter.UserId is not null)
            {
                commandFilter.AddCondition("UserId", filter.UserId);
            }
            if (filter.AccountId is not null)
            {
                commandFilter.AddCondition("AccountId", filter.AccountId);
            }

            return base.RetrieveCollectionAsync(commandFilter);
        }

        public async Task<bool> UpdateAsync(int objectId, UserAccountUpdate update)
        {
            using SqlConnection connection = await ConnectionFactory.CreateConnectionAsync();

            UpdateCommand updateCommand = new UpdateCommand(connection, "UserAccounts", IdDbFieldEnumeratorName, objectId);

            updateCommand.AddSetClause("AccountId", update.AccountId);

            return await updateCommand.ExecuteNonQueryAsync() > 0;
        }

        public Task<bool> DeleteAsync(int objectId) // won't work in this case
        {
            return base.DeleteAsync(IdDbFieldEnumeratorName, objectId);
        }

        public async Task<bool> DeleteAsync(int userId, int accountId)
        {
            using SqlConnection connection = await ConnectionFactory.CreateConnectionAsync();
            using SqlCommand command = connection.CreateCommand();
            using SqlTransaction transaction = connection.BeginTransaction();

            command.Transaction = transaction;
            command.CommandText = $"DELETE FROM {GetTableName()} WHERE UserId = @UserId AND AccountId = @AccountId";

            command.Parameters.AddWithValue("@UserId", userId);
            command.Parameters.AddWithValue("@AccountId", accountId);

            int rowsAffected = await command.ExecuteNonQueryAsync();

            if (rowsAffected != 1)
            {
                transaction.Rollback();
                throw new Exception($"Just one row should be deleted! Command aborted, because {rowsAffected} could have been deleted!");
            }

            transaction.Commit();
            return true;
        }
    }
}
