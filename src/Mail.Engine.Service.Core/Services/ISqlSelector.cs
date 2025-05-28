namespace Mail.Engine.Service.Core.Services
{
    public interface ISqlSelector
    {
        dynamic ExecuteAsyncProcQuery<T>(string query, object? parameters = null);

        Task<bool> ExecuteAsyncQuery(string query, object? parameters = null);
        Task<T?> ExecuteScalarAsyncQuery<T>(string query, object? parameters = null);

        Task<List<T>> SelectQuery<T>(string query, object? parameters = null) where T : new();
        Task<T?> SelectFirstOrDefaultQuery<T>(string query, object? parameters = null) where T : new();

        Task<T?> ExecuteStoredProcedureAsync<T>(string query, object? parameters);
    }
}
