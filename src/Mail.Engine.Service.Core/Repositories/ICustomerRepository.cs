using Mail.Engine.Service.Core.Entities;

namespace Mail.Engine.Service.Core.Repositories
{
    public interface ICustomerRepository
    {
        Task<List<CustomerEntity>> GetCustomerIdList();


        Task<List<CustomerSessionEntity>> GetCustomerSession(string? id);
        Task<bool> UpdateCustomerSession(Guid? id);

        Task<LoginKeyEntity> GetLoginKey(string id);
        Task<bool> RemoveLoginKey(string key);
    }
}
