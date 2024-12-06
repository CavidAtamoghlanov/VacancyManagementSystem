using VacancyManagementSystem.Application.Repositories.Concretes;
using VacancyManagementSystem.Domain.Entities.Concretes;
using VacancyManagementSystem.Persistence.Context;
using VacancyManagementSystem.Persistence.Repositories.Commons;

namespace VacancyManagementSystem.Persistence.Repositories.Concretes;

public class VacancyQuestionRepository : GenericRepository<VacancyQuestion, int>, IVacancyQuestionRepository
{
    public VacancyQuestionRepository(ApplicationDbContext context) : base(context)
    {
    }
}
