using VacancyManagementSystem.Application.Repositories.Concretes;
using VacancyManagementSystem.Domain.Entities.Concretes;
using VacancyManagementSystem.Persistence.Context;
using VacancyManagementSystem.Persistence.Repositories.Commons;

namespace VacancyManagementSystem.Persistence.Repositories.Concretes;

public class TestAnswerRepository : GenericRepository<TestAnswer, int>, ITestAnswerRepository
{
    public TestAnswerRepository(ApplicationDbContext context) : base(context)
    {
    }
}
