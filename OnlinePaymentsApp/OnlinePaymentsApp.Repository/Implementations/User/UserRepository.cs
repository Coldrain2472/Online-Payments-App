using Microsoft.Data.SqlClient;
using OnlinePaymentsApp.Repository.Base;
using OnlinePaymentsApp.Repository.Helpers;
using OnlinePaymentsApp.Repository.Interfaces.User;

namespace OnlinePaymentsApp.Repository.Implementations.User
{
    public class UserRepository : BaseRepository<Models.User>, IUserRepository
    {
        private const string IdDbFieldEnumeratorName = "UserId";

        protected override string GetTableName()
        {
            return "Users";
        }

        protected override string[] GetColumns() => new[]
    {
            IdDbFieldEnumeratorName,
            "Name",
            "Username",
            "Password"
        };

        protected override Models.User MapEntity(SqlDataReader reader)
        {
            return new Models.User
            {
                UserId = Convert.ToInt32(reader[IdDbFieldEnumeratorName]),
                Name = Convert.ToString(reader["Name"]),
                Username = Convert.ToString(reader["Username"]),
                Password = Convert.ToString(reader["Password"])
            };
        }

        public Task<int> CreateAsync(Models.User entity)
        {
            return base.CreateAsync(entity, IdDbFieldEnumeratorName);
        }

        public Task<Models.User> RetrieveAsync(int objectId)
        {
            return base.RetrieveAsync(IdDbFieldEnumeratorName, objectId);
        }

        public IAsyncEnumerable<Models.User> RetrieveCollectionAsync(UserFilter filter)
        {
            Filter commandFilter = new Filter();

            if (filter.Username is not null)
            {
                commandFilter.AddCondition("Username", filter.Username);
            }

            return base.RetrieveCollectionAsync(commandFilter);
        }

        public async Task<bool> UpdateAsync(int objectId, UserUpdate update)
        {
            using SqlConnection connection = await ConnectionFactory.CreateConnectionAsync();

            UpdateCommand updateCommand = new UpdateCommand(connection, "Users", IdDbFieldEnumeratorName, objectId);

            updateCommand.AddSetClause("Name", update.Name);
            updateCommand.AddSetClause("Username", update.Username);
            updateCommand.AddSetClause("Password", update.Password);

            return await updateCommand.ExecuteNonQueryAsync() > 0;
        }

        public Task<bool> DeleteAsync(int objectId)
        {
            return base.DeleteAsync(IdDbFieldEnumeratorName, objectId);
        }
    }
}
