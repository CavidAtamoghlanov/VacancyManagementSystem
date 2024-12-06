using VacancyManagementSystem.Application.Responses;
using VacancyManagementSystem.Domain.DTOs;

namespace VacancyManagementSystem.Application.Services;

public interface IVacancyService
{
    Task<Response> CreateVacancyAsync(AddVacancyDto vacancyDto);
    Task<Response> UpdateVacancyAsync(UpdateVacancyDto vacancyDto);
    Task<Response> DeleteVacancyAsync(int id);
    Task<Response> GetVacanciesAsync();
    Task<Response> SetVacancyStatusAsync(SetVacancyStatusDto vacancyStatusDto);
    Task<Response> FilterVacanciesAsync(FilterVacancyDto filterDto);
}
