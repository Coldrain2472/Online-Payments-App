using Microsoft.Data.SqlClient;
using OnlinePaymentsApp.Repository.Base;
using OnlinePaymentsApp.Repository.Helpers;
using OnlinePaymentsApp.Repository.Interfaces.Payment;

namespace OnlinePaymentsApp.Repository.Implementations.Payment
{
    public class PaymentRepository : BaseRepository<Models.Payment>, IPaymentRepository
    {
        private const string IdDbFieldEnumeratorName = "PaymentId";

        protected override string GetTableName()
        {
            return "Payments";
        }

        protected override string[] GetColumns() => new[]
        {
            IdDbFieldEnumeratorName,
            "FromAccountId",
            "ToAccountNumber",
            "Amount",
            "Reason",
            "Status",
            "CreatedAt",
            "CreatedByUserId"
        };

        protected override Models.Payment MapEntity(SqlDataReader reader)
        {
            return new Models.Payment
            {
                PaymentId = Convert.ToInt32(reader[IdDbFieldEnumeratorName]),
                FromAccountId = Convert.ToInt32(reader["FromAccountId"]),
                ToAccountNumber = Convert.ToString(reader["ToAccountNumber"]),
                Amount = Convert.ToDecimal(reader["Amount"]),
                Reason = Convert.ToString(reader["Reason"]),
                Status = Convert.ToString(reader["Status"]),
                CreatedAt = Convert.ToDateTime(reader["CreatedAt"]),
                CreatedByUserId = Convert.ToInt32(reader["CreatedByUserId"])
            };
        }

        public Task<int> CreateAsync(Models.Payment entity)
        {
            return base.CreateAsync(entity, IdDbFieldEnumeratorName);
        }

        public Task<Models.Payment> RetrieveAsync(int objectId)
        {
            return base.RetrieveAsync(IdDbFieldEnumeratorName, objectId);
        }

        public IAsyncEnumerable<Models.Payment> RetrieveCollectionAsync(PaymentFilter filter)
        {
            Filter commandFilter = new Filter();

            if (filter.CreatedAt is not null)
            {
                commandFilter.AddCondition("CreatedAt", filter.CreatedAt);
            }
            if (filter.FromAccountId is not null)
            {
                commandFilter.AddCondition("FromAccountId", filter.FromAccountId);
            }
            if (filter.ToAccountNumber is not null)
            {
                commandFilter.AddCondition("ToAccountNumber", filter.ToAccountNumber);
            }
            if (filter.Amount is not null)
            {
                commandFilter.AddCondition("Amount", filter.Amount);
            }
            if (filter.Status is not null)
            {
                commandFilter.AddCondition("Status", filter.Status);
            }
            if (filter.CreatedByUserId is not null)
            {
                commandFilter.AddCondition("CreatedByUserId", filter.CreatedByUserId);
            }

            return base.RetrieveCollectionAsync(commandFilter);
        }

        public async Task<bool> UpdateAsync(int objectId, PaymentUpdate update)
        {
            using SqlConnection connection = await ConnectionFactory.CreateConnectionAsync();

            UpdateCommand updateCommand = new UpdateCommand(connection, "Payments", IdDbFieldEnumeratorName, objectId);

            updateCommand.AddSetClause("Amount", update.Amount);
            updateCommand.AddSetClause("Status", update.Status);

            return await updateCommand.ExecuteNonQueryAsync() > 0;
        }

        public Task<bool> DeleteAsync(int objectId)
        {
            return base.DeleteAsync(IdDbFieldEnumeratorName, objectId);
        }
    }
}
