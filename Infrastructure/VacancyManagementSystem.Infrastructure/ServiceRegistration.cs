using Microsoft.Extensions.DependencyInjection;
using VacancyManagementSystem.Application.Mappings;
using VacancyManagementSystem.Application.Services;
using VacancyManagementSystem.Infrastructure.Services;

namespace VacancyManagementSystem.Infrastructure;

public static class ServiceRegistration
{
    public static void AddInfrastructureServices(this IServiceCollection services)
    {
        services.AddScoped<IVacancyService, VacancyService>();
        services.AddScoped<IAnswerOptionService, AnswerOptionService>();
        services.AddScoped<IApplicantService, ApplicantService>();
        services.AddScoped<ITestService, TestService>();
        services.AddScoped<IVacancyQuestionService, VacancyQuestionService>();

        services.AddScoped<ITokenService, TokenService>();
        services.AddScoped<IAuthService, AuthService>();

    }
}