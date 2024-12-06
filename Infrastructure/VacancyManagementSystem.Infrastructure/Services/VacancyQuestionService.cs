using VacancyManagementSystem.Application;
using VacancyManagementSystem.Application.Mappings.Abstracts;
using VacancyManagementSystem.Application.Responses;
using VacancyManagementSystem.Application.Services;
using VacancyManagementSystem.Domain.DTOs;
using VacancyManagementSystem.Domain.Entities.Concretes;
using VacancyManagementSystem.Infrastructure.Services.Commons;

namespace VacancyManagementSystem.Infrastructure.Services
{
    public class VacancyQuestionService : BaseService, IVacancyQuestionService
    {
        public VacancyQuestionService(IUnitOfWork unitOfWork, IAutoMapperConfiguration autoMapper) : base(unitOfWork, autoMapper)
        {
        }

        public async Task<Response> GetQuestionsForVacancyAsync(int vacancyId)
        {
            var vacancyQuestionRepository = _unitOfWork.GetRepository<VacancyQuestion, int>();
            var questions = (await vacancyQuestionRepository.GetAllAsync())
                            .Where(q => q.VacancyId == vacancyId)
                            .ToList();

            if (questions is null || !questions.Any())
                return NotFound("No questions found for this vacancy.");

            var vacancyQuestionDtos = _autoMapper.Map<GetVacancyQuestionDto, VacancyQuestion>(questions);
            return Success(vacancyQuestionDtos);
        }

        public async Task<Response> GetQuestionByIdAsync(int id)
        {
            var vacancyQuestionRepository = _unitOfWork.GetRepository<VacancyQuestion, int>();
            var question = await vacancyQuestionRepository.GetByIdAsync(id);

            if (question is null)
                return NotFound("Question not found.");

            var vacancyQuestionDto = _autoMapper.Map<GetVacancyQuestionDto, VacancyQuestion>(question);
            return Success(vacancyQuestionDto);
        }

        public async Task<Response> CreateQuestionAsync(AddVacancyQuestionDto questionDto)
        {
            var question = _autoMapper.Map<VacancyQuestion, AddVacancyQuestionDto>(questionDto);

            var vacancyQuestionRepository = _unitOfWork.GetRepository<VacancyQuestion, int>();
            await vacancyQuestionRepository.AddAsync(question);
            await _unitOfWork.Commit();

            return Success("Question successfully created.");
        }

        public async Task<Response> UpdateQuestionAsync(UpdateVacancyQuestionDto questionDto)
        {
            var vacancyQuestionRepository = _unitOfWork.GetRepository<VacancyQuestion, int>();
            var question = await vacancyQuestionRepository.GetByIdAsync(questionDto.Id);

            if (question is null)
                return NotFound("Question not found.");

            question = _autoMapper.Map<VacancyQuestion, UpdateVacancyQuestionDto>(questionDto);
            vacancyQuestionRepository.Update(question);
            await _unitOfWork.Commit();

            return Success("Question successfully updated.");
        }

        public async Task<Response> DeleteQuestionAsync(int id)
        {
            var vacancyQuestionRepository = _unitOfWork.GetRepository<VacancyQuestion, int>();
            var question = await vacancyQuestionRepository.GetByIdAsync(id);

            if (question is null)
                return NotFound("Question not found.");

            vacancyQuestionRepository.Delete(question);
            await _unitOfWork.Commit();

            return Success("Question successfully deleted.");
        }
    }
}
