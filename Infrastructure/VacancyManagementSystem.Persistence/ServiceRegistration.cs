using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using VacancyManagementSystem.Domain.Entities.Concretes;
using VacancyManagementSystem.Persistence.Context;
using Microsoft.AspNetCore.Identity;
using VacancyManagementSystem.Application.Repositories.Concretes;
using VacancyManagementSystem.Persistence.Repositories.Concretes;
using VacancyManagementSystem.Application;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;
using VacancyManagementSystem.Application.DapperRepositories;
using VacancyManagementSystem.Persistence.DapperRepositories;

namespace VacancyManagementSystem.Persistence;

public static class ServiceRegistration
{
    public static void AddPersistenceServices(this IServiceCollection services, string connectionString)
    {
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(connectionString));


        services.AddIdentity<ApplicationUser, ApplicationRole>()
                                    .AddEntityFrameworkStores<ApplicationDbContext>()
                                    .AddDefaultTokenProviders();

        // Repositories
        services.AddScoped<IAnswerOptionRepository, AnswerOptionRepository>();
        services.AddScoped<IApplicantRepository, ApplicantRepository>();
        services.AddScoped<ICategoryRepository, CategoryRepository>();
        services.AddScoped<IVacancyRepository, VacancyRepository>();
        services.AddScoped<IVacancyQuestionRepository, VacancyQuestionRepository>();
        services.AddScoped<IQuestionBankRepository, QuestionBankRepository>();
        services.AddScoped<ITestAnswerRepository, TestAnswerRepository>();

        // IDbConnection
        services.AddScoped<IDbConnection>(db => new SqlConnection(connectionString));

        // DapperRepository
        services.AddScoped<IDapperRepository, DapperRepository>();

        // UnitOfWork
        services.AddScoped<IUnitOfWork, UnitOfWork>();
    }
}