using Mail.Engine.Service.Core.Entities;

namespace Mail.Engine.Service.Core.Repositories
{
    public interface IWatiRepository
    {
        Task<WatiConfigEntity> GetWatiConfig();

        Task<List<MessageLogEntity>> GetMessageLogs();

        Task<bool> InsertJsonData(Guid messageLogId, string responseJson);
    }
}
