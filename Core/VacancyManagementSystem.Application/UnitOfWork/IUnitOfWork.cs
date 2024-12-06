using VacancyManagementSystem.Application.DapperRepositories;
using VacancyManagementSystem.Application.Repositories.Commons;
using VacancyManagementSystem.Application.Repositories.Concretes;
using VacancyManagementSystem.Domain.Entities.Abstacts;

namespace VacancyManagementSystem.Application;

public interface IUnitOfWork
{
    IGenericRepository<TEntity, TKey> GetRepository<TEntity, TKey>()
        where TEntity : class, IBaseEntity<TKey>;

    Task Commit();
}
