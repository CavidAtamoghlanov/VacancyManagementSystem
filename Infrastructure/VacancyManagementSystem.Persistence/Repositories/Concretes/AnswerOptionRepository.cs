using VacancyManagementSystem.Application.Repositories.Concretes;
using VacancyManagementSystem.Domain.Entities.Concretes;
using VacancyManagementSystem.Persistence.Context;
using VacancyManagementSystem.Persistence.Repositories.Commons;

namespace VacancyManagementSystem.Persistence.Repositories.Concretes;

public class AnswerOptionRepository : GenericRepository<AnswerOption, int>, IAnswerOptionRepository
{
    public AnswerOptionRepository(ApplicationDbContext context) : base(context)
    {
    }
}
