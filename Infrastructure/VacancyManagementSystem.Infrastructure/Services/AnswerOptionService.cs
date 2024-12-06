using VacancyManagementSystem.Application;
using VacancyManagementSystem.Application.Mappings.Abstracts;
using VacancyManagementSystem.Application.Responses;
using VacancyManagementSystem.Application.Services;
using VacancyManagementSystem.Domain.DTOs;
using VacancyManagementSystem.Domain.Entities.Concretes;
using VacancyManagementSystem.Infrastructure.Services.Commons;

namespace VacancyManagementSystem.Infrastructure.Services;

public class AnswerOptionService : BaseService, IAnswerOptionService
{
    public AnswerOptionService(IUnitOfWork unitOfWork, IAutoMapperConfiguration autoMapper) : base(unitOfWork, autoMapper)
    {
    }

    public async Task<Response> GetAnswerOptionsForQuestion(int questionId)
    {
        var answerOptionRepository = _unitOfWork.GetRepository<AnswerOption, int>();
        var answerOptions = await answerOptionRepository.GetAllAsync();

        var optionsForQuestion = answerOptions.Where(o => o.QuestionBankId == questionId).ToList();

        if (!optionsForQuestion.Any())
            return NotFound("No answer options found for the specified question.");

        var answerOptionDtos = _autoMapper.Map<List<AnswerOptionDto>, List<AnswerOption>>(optionsForQuestion);

        return Success(answerOptionDtos);
    }

    public async Task<Response> GetAnswerOptionById(int id)
    {
        var answerOptionRepository = _unitOfWork.GetRepository<AnswerOption, int>();
        var answerOption = await answerOptionRepository.GetByIdAsync(id);

        if (answerOption == null)
            return NotFound("Answer option not found.");

        var answerOptionDto = _autoMapper.Map<AnswerOptionDto, AnswerOption>(answerOption);
        return Success(answerOptionDto);
    }

    public async Task<Response> CreateAnswerOption(AnswerOptionDto answerOptionDto)
    {
        var answerOption = _autoMapper.Map<AnswerOption, AnswerOptionDto>(answerOptionDto);
        var answerOptionRepository = _unitOfWork.GetRepository<AnswerOption, int>();
        await answerOptionRepository.AddAsync(answerOption);
        await _unitOfWork.Commit();
        return Success("Answer option successfully created.");
    }

    public async Task<Response> UpdateAnswerOption(UpdateAnswerOptionDto answerOptionDto)
    {
        var answerOptionRepository = _unitOfWork.GetRepository<AnswerOption, int>();
        var answerOption = await answerOptionRepository.GetByIdAsync(answerOptionDto.Id);

        if (answerOption == null)
            return NotFound("Answer option not found.");

        answerOption = _autoMapper.Map<AnswerOption, UpdateAnswerOptionDto>(answerOptionDto);

        answerOptionRepository.Update(answerOption);
        await _unitOfWork.Commit();

        return Success("Answer option successfully updated.");
    }


    public async Task<Response> DeleteAnswerOption(int id)
    {
        var answerOptionRepository = _unitOfWork.GetRepository<AnswerOption, int>();
        var answerOption = await answerOptionRepository.GetByIdAsync(id);

        if (answerOption == null)
            return NotFound("Answer option not found.");

        answerOption.IsDeleted = true;
        answerOptionRepository.Update(answerOption);
        await _unitOfWork.Commit();
        return Success("Answer option successfully deleted.");
    }

    public async Task<Response> RestoreAnswerOption(int id)
    {
        var answerOptionRepository = _unitOfWork.GetRepository<AnswerOption, int>();
        var answerOption = await answerOptionRepository.GetByIdAsync(id);

        if (answerOption == null)
            return NotFound("Answer option not found.");

        if (!answerOption.IsDeleted)
            return BadRequest("Answer option is not deleted and cannot be restored.");

        answerOption.IsDeleted = false;
        answerOptionRepository.Update(answerOption);
        await _unitOfWork.Commit();

        return Success("Answer option successfully restored.");
    }
}
