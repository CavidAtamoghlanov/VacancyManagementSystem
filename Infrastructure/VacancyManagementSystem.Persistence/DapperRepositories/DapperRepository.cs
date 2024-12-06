using Dapper;
using System.Data;
using VacancyManagementSystem.Application.DapperRepositories;
using VacancyManagementSystem.Domain.Entities.Abstacts;

namespace VacancyManagementSystem.Persistence.DapperRepositories
{
    public class DapperRepository : IDapperRepository
    {
        private readonly IDbConnection _dbConnection;

        public DapperRepository(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public async Task<IEnumerable<T>> GetAllAsync<T>(string query, object? parameters = null) where T : class, IBaseEntity<int>
        {
            return await _dbConnection.QueryAsync<T>(query, parameters);
        }

        public async Task<T?> GetByIdAsync<T>(string query, object parameters) where T : class, IBaseEntity<int>
        {
            return await _dbConnection.QuerySingleOrDefaultAsync<T>(query, parameters);
        }

        public async Task<int> ExecuteAsync(string query, object parameters)
        {
            return await _dbConnection.ExecuteAsync(query, parameters);
        }

        public async Task<IEnumerable<T>> GetFromViewAsync<T>(string viewName, object? parameters = null) where T : class, IBaseEntity<int>
        {
            var query = $"SELECT * FROM {viewName}";
            return await _dbConnection.QueryAsync<T>(query, parameters);
        }

        public async Task<IEnumerable<T>> ExecuteStoredProcedureAsync<T>(string storedProcedure, object parameters = null) where T : class, IBaseEntity<int>
        {
            return await _dbConnection.QueryAsync<T>(
                storedProcedure,
                parameters,
                commandType: CommandType.StoredProcedure
            );
        }

        public async Task<T?> ExecuteStoredProcedureSingleAsync<T>(string storedProcedure, object parameters = null) where T : class, IBaseEntity<int>
        {
            return await _dbConnection.QuerySingleOrDefaultAsync<T>(
                storedProcedure,
                parameters,
                commandType: CommandType.StoredProcedure
            );
        }
    }
}
