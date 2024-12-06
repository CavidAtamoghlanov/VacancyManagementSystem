using VacancyManagementSystem.Application.Responses;
using VacancyManagementSystem.Domain.DTOs;

namespace VacancyManagementSystem.Application.Services;
public interface ITestService
{
    Task<Response> GetTestResultsAsync(int applicantId);
    Task<Response> GetTestQuestionsAsync(int vacancyId);
    Task<Response> SubmitTestAsync(List<TestAnswerDto> testAnswers);
    Task<Response> ResumeTestAsync(int applicantId);
}