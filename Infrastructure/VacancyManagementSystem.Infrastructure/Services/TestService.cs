using VacancyManagementSystem.Application;
using VacancyManagementSystem.Application.Mappings.Abstracts;
using VacancyManagementSystem.Application.Responses;
using VacancyManagementSystem.Application.Services;
using VacancyManagementSystem.Domain.DTOs;
using VacancyManagementSystem.Domain.Entities.Concretes;
using VacancyManagementSystem.Infrastructure.Services.Commons;

namespace VacancyManagementSystem.Infrastructure.Services
{
    public class TestService : BaseService, ITestService
    {
        public TestService(IUnitOfWork unitOfWork, IAutoMapperConfiguration autoMapper) : base(unitOfWork, autoMapper)
        {
        }

        public async Task<Response> GetTestQuestionsAsync(int vacancyId)
        {
            var vacancyQuestionRepository = _unitOfWork.GetRepository<VacancyQuestion, int>();
            var questions = (await vacancyQuestionRepository.GetAllAsync())
                .Where(q => q.VacancyId == vacancyId).ToList();

            if (questions == null || !questions.Any())
                return NotFound("No questions found for this vacancy.");

            var questionDtos = questions.Select(q => new GetTestQuestionDto
            {
                Id = q.Id,
                QuestionText = q.QuestionBank.QuestionText,
                AnswerOptions = q.QuestionBank.AnswerOptions.Select(a => new GetAnswerOptionDto
                {
                    Id = a.Id,
                    OptionText = a.OptionText,
                    IsCorrect = a.IsCorrect
                }).ToList()
            }).ToList();

            return Success(questionDtos);
        }

        public async Task<Response> GetTestResultsAsync(int applicantId)
        {
            var testAnswerRepository = _unitOfWork.GetRepository<TestAnswer, int>();
            var testAnswers = (await testAnswerRepository.GetAllAsync())
                .Where(t => t.ApplicantId == applicantId).ToList();

            if (testAnswers == null || !testAnswers.Any())
                return NotFound("No test results found for this applicant.");

            var resultDto = new GetTestResultDto
            {
                ApplicantId = applicantId,
                TestAnswers = testAnswers.Select(t => new GetTestAnswerDto
                {
                    QuestionId = t.VacancyQuestionId,
                    QuestionText = t.VacancyQuestion.QuestionBank.QuestionText,
                    AnswerOptionText = t.AnswerOption.OptionText,
                    IsCorrect = t.IsCorrect
                }).ToList(),
                CorrectAnswers = testAnswers.Count(t => t.IsCorrect),
                TotalQuestions = testAnswers.Count
            };

            return Success(resultDto);
        }

        public async Task<Response> ResumeTestAsync(int applicantId)
        {
            var testAnswerRepository = _unitOfWork.GetRepository<TestAnswer, int>();
            var testAnswers = (await testAnswerRepository.GetAllAsync())
                .Where(t => t.ApplicantId == applicantId).ToList();

            if (testAnswers == null || !testAnswers.Any())
                return NotFound("Test not found for this applicant.");

            var unansweredQuestions = testAnswers
                .Where(t => !t.IsCorrect)
                .Select(t => new ResumeTestDto
                {
                    QuestionId = t.VacancyQuestionId,
                    QuestionText = t.VacancyQuestion.QuestionBank.QuestionText,
                    AnswerOptions = t.VacancyQuestion.QuestionBank.AnswerOptions.Select(a => new GetAnswerOptionDto
                    {
                        Id = a.Id,
                        OptionText = a.OptionText,
                        IsCorrect = a.IsCorrect
                    }).ToList()
                }).ToList();

            return Success(unansweredQuestions);
        }

        public async Task<Response> SubmitTestAsync(List<TestAnswerDto> testAnswers)
        {
            if (testAnswers == null || !testAnswers.Any())
                return BadRequest("No test answers provided.");

            var testAnswerRepository = _unitOfWork.GetRepository<TestAnswer, int>();

            foreach (var answer in testAnswers)
            {
                var testAnswer = new TestAnswer
                {
                    ApplicantId = answer.ApplicantId,
                    VacancyQuestionId = answer.VacancyQuestionId,
                    AnswerOptionId = answer.AnswerOptionId,
                    IsCorrect = answer.IsCorrect
                };
                await testAnswerRepository.AddAsync(testAnswer);
            }

            await _unitOfWork.Commit();

            var correctAnswers = testAnswers.Count(a => a.IsCorrect);
            var totalAnswers = testAnswers.Count;

            var result = new { CorrectAnswers = correctAnswers, TotalQuestions = totalAnswers };
            return Success(result);
        }
    }
}
