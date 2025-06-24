using Mail.Engine.Service.Core.Entities;
using Mail.Engine.Service.Core.Repositories;
using Mail.Engine.Service.Core.Services;
using Mail.Engine.Service.Infrastructure.DbQueries;

namespace Mail.Engine.Service.Infrastructure.Repositories
{
    public class CustomerRepository(ISqlSelector sqlContext) : ICustomerRepository
    {
        private readonly ISqlSelector _sqlContext = sqlContext;

        public async Task<List<CustomerEntity>> GetCustomerIdList()
        {
            var query = CustomerQuery.CustomerIdListQuery();

            var items = await _sqlContext.SelectQuery<CustomerEntity>(query);

            return [.. items];
        }

        public async Task<List<CustomerSessionEntity>> GetCustomerSession(string? id)
        {
            var query = CustomerQuery.GetCustomerSessionQuery();

            var items = await _sqlContext.SelectQuery<CustomerSessionEntity>(query, new { CustomerId = id });

            return [.. items];
        }

        public async Task<bool> UpdateCustomerSession(Guid? id)
        {
            var query = CustomerQuery.UpdateUserSessionQuery();

            var result = await _sqlContext.ExecuteAsyncQuery(query, new { UserSessionId = id });

            return result;
        }

        public async Task<LoginKeyEntity> GetLoginKey(string id)
        {
            var query = CustomerQuery.GetLoginKeyQuery();

            var item = await _sqlContext.SelectFirstOrDefaultQuery<LoginKeyEntity>(query, new { CustomerId = id });

            return item!;
        }

        public async Task<bool> RemoveLoginKey(string key)
        {
            var query = CustomerQuery.RemoveLoginKeyQuery();

            var result = await _sqlContext.ExecuteAsyncQuery(query, new { Key = key });

            return result;
        }
    }
}
