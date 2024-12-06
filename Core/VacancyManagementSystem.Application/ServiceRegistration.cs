using Microsoft.Extensions.DependencyInjection;
using VacancyManagementSystem.Application.Mappings;
using VacancyManagementSystem.Application.Mappings.Abstracts;
using VacancyManagementSystem.Application.Mappings.Concretes;

namespace VacancyManagementSystem.Application;

public static class ServiceRegistration
{
    public static void AddApplicationServices(this IServiceCollection services)
    {
        services.AddScoped<IAutoMapperConfiguration, AutoMapperConfiguration>();
    }
}