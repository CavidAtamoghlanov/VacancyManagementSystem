using VacancyManagementSystem.Application.Responses;
using VacancyManagementSystem.Domain.DTOs;

namespace VacancyManagementSystem.Application.Services;

public interface IAnswerOptionService
{
    Task<Response> GetAnswerOptionsForQuestion(int questionId);
    Task<Response> GetAnswerOptionById(int id);
    Task<Response> CreateAnswerOption(AnswerOptionDto answerOptionDto);
    Task<Response> UpdateAnswerOption(UpdateAnswerOptionDto answerOptionDto);
    Task<Response> DeleteAnswerOption(int id);
    Task<Response> RestoreAnswerOption(int id);
}