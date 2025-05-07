using Mail.Engine.Service.Core.Entities;
using Mail.Engine.Service.Core.Repositories;
using Mail.Engine.Service.Core.Services;
using Mail.Engine.Service.Infrastructure.DbQueries;

namespace Mail.Engine.Service.Infrastructure.Repositories
{
    public class WatiRepository(ISqlSelector sqlContext) : IWatiRepository
    {
        private readonly ISqlSelector _sqlContext = sqlContext;

        public Task<WatiConfigEntity> GetWatiConfig()
        {
            var query = WatiQuery.GetWatiConfigQuery();

            return _sqlContext.SelectFirstOrDefaultQuery<WatiConfigEntity>(query)!;
        }
    }
}
