using VacancyManagementSystem.Application.Responses;
using VacancyManagementSystem.Domain.DTOs;

namespace VacancyManagementSystem.Application.Services;

public interface IVacancyQuestionService
{
    Task<Response> GetQuestionsForVacancyAsync(int vacancyId);
    Task<Response> GetQuestionByIdAsync(int id);
    Task<Response> CreateQuestionAsync(AddVacancyQuestionDto questionDto);
    Task<Response> UpdateQuestionAsync(UpdateVacancyQuestionDto questionDto);
    Task<Response> DeleteQuestionAsync(int id);
}