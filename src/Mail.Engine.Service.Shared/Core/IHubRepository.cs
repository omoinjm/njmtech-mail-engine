using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mail.Engine.Service.Shared.Core
{
    public interface IHubRepository
    {
        Task<List<> GetMailboxes();
    }
}