using Microsoft.AspNetCore.Mvc;
using VacancyManagementSystem.Application.Services;
using VacancyManagementSystem.Domain.DTOs;

namespace VacancyManagementSystem.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AnswerOptionController : ControllerBase
{
    private readonly IAnswerOptionService _answerOptionService;

    public AnswerOptionController(IAnswerOptionService answerOptionService)
    {
        _answerOptionService = answerOptionService;
    }

    [HttpGet("[action]/{questionId}")]
    public async Task<IActionResult> GetAnswerOptionsForQuestion(int questionId)
    {
        var response = await _answerOptionService.GetAnswerOptionsForQuestion(questionId);
        return StatusCode(response.StatusCode, response);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetAnswerOptionById(int id)
    {
        var response = await _answerOptionService.GetAnswerOptionById(id);
        return StatusCode(response.StatusCode, response);
    }

    [HttpPost("[action]")]
    public async Task<IActionResult> CreateAnswerOption([FromBody] AnswerOptionDto answerOptionDto)
    {
        var response = await _answerOptionService.CreateAnswerOption(answerOptionDto);
        return StatusCode(response.StatusCode, response);
    }

    [HttpPut("[action]/{id}")]
    public async Task<IActionResult> UpdateAnswerOption([FromBody] UpdateAnswerOptionDto answerOptionDto)
    {
        var response = await _answerOptionService.UpdateAnswerOption(answerOptionDto);
        return StatusCode(response.StatusCode, response);
    }

    [HttpDelete("Delete/{id}")]
    public async Task<IActionResult> DeleteAnswerOption(int id)
    {
        var response = await _answerOptionService.DeleteAnswerOption(id);
        return StatusCode(response.StatusCode, response);
    }

    [HttpPost("Restore/{id}")]
    public async Task<IActionResult> RestoreAnswerOption(int id)
    {
        var response = await _answerOptionService.RestoreAnswerOption(id);
        return StatusCode(response.StatusCode, response);
    }
}

