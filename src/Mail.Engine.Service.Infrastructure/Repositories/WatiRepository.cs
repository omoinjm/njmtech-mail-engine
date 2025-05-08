using Mail.Engine.Service.Core.Entities;
using Mail.Engine.Service.Core.Helpers;
using Mail.Engine.Service.Core.Repositories;
using Mail.Engine.Service.Core.Results.Wati;
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

        public async Task<List<MessageLogEntity>> GetMessageLogs()
        {
            var items = await _sqlContext.SelectQuery<MessageLogEntity>(WatiQuery.GetMessageLogsQuery());

            return [.. items];
        }


        public async Task<bool> InsertJsonData(Guid messageLogId, string responseJson)
        {
            var parameters = MailMessageHelper.InsertWatiResponseParameters(messageLogId, responseJson);

            var result = await _sqlContext.ExecuteAsyncQuery(WatiQuery.InsertJsonData(), parameters);

            return result;
        }
    }
}
