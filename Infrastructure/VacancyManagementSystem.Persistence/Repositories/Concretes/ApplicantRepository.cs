using VacancyManagementSystem.Application.Repositories.Concretes;
using VacancyManagementSystem.Domain.Entities.Concretes;
using VacancyManagementSystem.Persistence.Context;
using VacancyManagementSystem.Persistence.Repositories.Commons;

namespace VacancyManagementSystem.Persistence.Repositories.Concretes;

public class ApplicantRepository : GenericRepository<Applicant, int>, IApplicantRepository
{
    public ApplicantRepository(ApplicationDbContext context) : base(context)
    {
    }
}
