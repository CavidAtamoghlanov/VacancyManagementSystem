using VacancyManagementSystem.Application;
using VacancyManagementSystem.Application.DapperRepositories;
using VacancyManagementSystem.Application.Repositories.Commons;
using VacancyManagementSystem.Application.Repositories.Concretes;
using VacancyManagementSystem.Persistence.Context;
using VacancyManagementSystem.Persistence.Repositories.Commons;

namespace VacancyManagementSystem.Persistence;

public class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationDbContext _context;

    private Dictionary<Type, object> _repositories;

    public UnitOfWork(ApplicationDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
        _repositories = new Dictionary<Type, object>();
    }

    public async Task Commit()
    {
            await _context.SaveChangesAsync();
    }

    IGenericRepository<TEntity, TKey> IUnitOfWork.GetRepository<TEntity, TKey>()
    {
        if (_repositories.ContainsKey(typeof(TEntity)))
            return (IGenericRepository<TEntity, TKey>)_repositories[typeof(TEntity)];

        var repository = new GenericRepository<TEntity, TKey>(_context);
        _repositories.Add(typeof(TEntity), repository);
        return repository;
    }
}

