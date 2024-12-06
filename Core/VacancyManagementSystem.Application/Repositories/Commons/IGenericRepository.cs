using System.Linq.Expressions;
using VacancyManagementSystem.Domain.Entities.Abstacts;

namespace VacancyManagementSystem.Application.Repositories.Commons;

public interface IGenericRepository<T, TKey> where T : class, IBaseEntity<TKey>
{
    Task<T?> GetByIdAsync(TKey id, bool tracking = false);
    Task<T?> GetFirstOrDefaultAsync(Expression<Func<T, bool>> filter, bool tracking = false, params Expression<Func<T, object>>[] includes);
    Task<IQueryable<T>> GetAllAsync(Expression<Func<T, bool>>? filter = null, bool tracking = false, Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null, params Expression<Func<T, object>>[] includes);

    Task<(IQueryable<T> Items, int TotalCount)> GetPagedAsync(int pageIndex, int pageSize, bool tracking = false, Expression<Func<T, bool>>? filter = null, Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null, params Expression<Func<T, object>>[] includes);

    Task AddAsync(T entity);
    Task AddRangeAsync(IEnumerable<T> entities);
    void Update(T entity);
    void Delete(T entity);
    void DeleteRange(IEnumerable<T> entities);

    Task<int> SaveChangesAsync();
}