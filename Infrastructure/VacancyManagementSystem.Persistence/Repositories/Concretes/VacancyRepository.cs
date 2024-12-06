using VacancyManagementSystem.Application.Repositories.Concretes;
using VacancyManagementSystem.Domain.Entities.Concretes;
using VacancyManagementSystem.Persistence.Context;
using VacancyManagementSystem.Persistence.Repositories.Commons;

namespace VacancyManagementSystem.Persistence.Repositories.Concretes;

public class VacancyRepository : GenericRepository<Vacancy, int>, IVacancyRepository
{
    public VacancyRepository(ApplicationDbContext context) : base(context)
    {
    }
}
