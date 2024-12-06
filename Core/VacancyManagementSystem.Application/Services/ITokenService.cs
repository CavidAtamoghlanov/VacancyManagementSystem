using VacancyManagementSystem.Domain.Entities.Concretes;

namespace VacancyManagementSystem.Application.Services;

public interface ITokenService
{
    string GenerateAccessToken(ApplicationUser user);
}