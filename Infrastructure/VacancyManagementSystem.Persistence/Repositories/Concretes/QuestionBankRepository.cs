using VacancyManagementSystem.Application.Repositories.Concretes;
using VacancyManagementSystem.Domain.Entities.Concretes;
using VacancyManagementSystem.Persistence.Context;
using VacancyManagementSystem.Persistence.Repositories.Commons;

namespace VacancyManagementSystem.Persistence.Repositories.Concretes;

public class QuestionBankRepository : GenericRepository<QuestionBank, int>, IQuestionBankRepository
{
    public QuestionBankRepository(ApplicationDbContext context) : base(context)
    {
    }
}
