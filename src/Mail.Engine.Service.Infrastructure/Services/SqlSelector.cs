using System.Data;
using Dapper;
using Mail.Engine.Service.Core.Services;
using Npgsql;

namespace Mail.Engine.Service.Infrastructure.Services
{
    public class SqlSelector(string connectionString) : ISqlSelector
    {
        private readonly string _connectionString = connectionString;

        // For multiple records query
        public async Task<List<T>> SelectQuery<T>(string query, object? parameters = null) where T : new()
        {
            using var db = new NpgsqlConnection(_connectionString);

            var result = await db.QueryAsync<T>(query, parameters);

            return [.. result];
        }

        // For single record query
        public async Task<T?> SelectFirstOrDefaultQuery<T>(string query, object? parameters = null) where T : new()
        {
            using var db = new NpgsqlConnection(_connectionString);

            var result = await db.QueryFirstOrDefaultAsync<T>(query, parameters);

            return result;
        }

        // For update and delete queries
        public async Task<bool> ExecuteAsyncQuery(string query, object? parameters = null)
        {
            using var db = new NpgsqlConnection(_connectionString);

            await db.OpenAsync();

            var affected = await db.ExecuteAsync(query, parameters);

            return affected != 0;
        }

        // For creation queries
        public async Task<T?> ExecuteScalarAsyncQuery<T>(string query, object? parameters = null)
        {
            using var db = new NpgsqlConnection(_connectionString);

            var result = await db.ExecuteScalarAsync<T>(query, parameters);

            return result;
        }

        // For stored procedures
        public dynamic ExecuteAsyncProcQuery<T>(string query, object? parameters = null)
        {
            using var db = new NpgsqlConnection(_connectionString);

            return db.Query<T>(query, parameters, commandType: CommandType.StoredProcedure);
        }
    }
}