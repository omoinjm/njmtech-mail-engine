using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mail.Engine.Service.Shared.Core
{
    public interface ISqlSelector
    {
        dynamic ExecuteAsyncProcQuery<T>(string query, object? parameters = null);

        Task<bool> ExecuteAsyncQuery(string query, object? parameters = null);
        Task<T?> ExecuteScalarAsyncQuery<T>(string query, object? parameters = null);

        Task<List<T>> SelectQuery<T>(string query, object? parameters = null) where T : new();
        Task<T?> SelectFirstOrDefaultQuery<T>(string query, object? parameters = null) where T : new();
    }
}