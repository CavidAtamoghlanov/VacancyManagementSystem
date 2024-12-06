using Microsoft.AspNetCore.Mvc;
using VacancyManagementSystem.Application.Services;
using VacancyManagementSystem.Domain.DTOs;

namespace VacancyManagementSystem.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TestController : ControllerBase
{
    private readonly ITestService _testService;

    public TestController(ITestService testService)
    {
        _testService = testService;
    }

    [HttpGet("[action]/{vacancyId}")]
    public async Task<IActionResult> GetTestQuestions(int vacancyId)
    {
        var response = await _testService.GetTestQuestionsAsync(vacancyId);
        return Ok(response);
    }

    [HttpGet("[action]/{applicantId}")]
    public async Task<IActionResult> GetTestResults(int applicantId)
    {
        var response = await _testService.GetTestResultsAsync(applicantId);
        return Ok(response);
    }

    [HttpGet("[action]/{applicantId}")]
    public async Task<IActionResult> ResumeTest(int applicantId)
    {
        var response = await _testService.ResumeTestAsync(applicantId);
        return Ok(response);
    }

    [HttpPost("[action]")]
    public async Task<IActionResult> SubmitTest([FromBody] List<TestAnswerDto> testAnswers)
    {
        var response = await _testService.SubmitTestAsync(testAnswers);
        return Ok(response);
    }
}
