using Microsoft.AspNetCore.Mvc;
using VacancyManagementSystem.Application.Services;
using VacancyManagementSystem.Domain.DTOs;

namespace VacancyManagementSystem.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class VacancyQuestionController : ControllerBase
{
    private readonly IVacancyQuestionService _vacancyQuestionService;

    public VacancyQuestionController(IVacancyQuestionService vacancyQuestionService)
    {
        _vacancyQuestionService = vacancyQuestionService;
    }

    [HttpGet("[action]/{vacancyId}")]
    public async Task<IActionResult> GetQuestionsForVacancy(int vacancyId)
    {
        var response = await _vacancyQuestionService.GetQuestionsForVacancyAsync(vacancyId);
        return Ok(response);
    }

    [HttpGet("[action]/{id}")]
    public async Task<IActionResult> GetQuestionById(int id)
    {
        var response = await _vacancyQuestionService.GetQuestionByIdAsync(id);
        return Ok(response);
    }

    [HttpPost("[action]")]
    public async Task<IActionResult> CreateQuestion([FromBody] AddVacancyQuestionDto questionDto)
    {
        var response = await _vacancyQuestionService.CreateQuestionAsync(questionDto);
        return Ok(response);
    }

    [HttpPut("[action]")]
    public async Task<IActionResult> UpdateQuestion([FromBody] UpdateVacancyQuestionDto questionDto)
    {
        var response = await _vacancyQuestionService.UpdateQuestionAsync(questionDto);
        return Ok(response);
    }

    [HttpDelete("[action]/{id}")]
    public async Task<IActionResult> DeleteQuestion(int id)
    {
        var response = await _vacancyQuestionService.DeleteQuestionAsync(id);
        return Ok(response);
    }
}