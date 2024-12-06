
using VacancyManagementSystem.Domain.Entities.Abstacts;

namespace VacancyManagementSystem.Application.DapperRepositories;

public interface IDapperRepository
{
    Task<IEnumerable<T>> GetAllAsync<T>(string query, object? parameters = null) where T : class, IBaseEntity<int>;

    Task<T?> GetByIdAsync<T>(string query, object parameters) where T : class, IBaseEntity<int>;

    Task<int> ExecuteAsync(string query, object parameters);

    Task<IEnumerable<T>> ExecuteStoredProcedureAsync<T>(string storedProcedure, object parameters = null) where T : class, IBaseEntity<int>;

    Task<T?> ExecuteStoredProcedureSingleAsync<T>(string storedProcedure, object parameters = null) where T : class, IBaseEntity<int>;

    Task<IEnumerable<T>> GetFromViewAsync<T>(string viewName, object? parameters = null) where T : class, IBaseEntity<int>;
}