using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VacancyManagementSystem.Application.Services;
using VacancyManagementSystem.Domain.DTOs;

namespace VacancyManagementSystem.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class VacancyController : ControllerBase
{
    private readonly IVacancyService _vacancyService;

    public VacancyController(IVacancyService vacancyService)
    {
        _vacancyService = vacancyService;
    }

    [HttpPost("[action]")]
    public async Task<IActionResult> CreateVacancy([FromBody] AddVacancyDto vacancyDto)
    {
        var response = await _vacancyService.CreateVacancyAsync(vacancyDto);
        return Ok(response);
    }

    [HttpPut("[action]/{id}")]
    public async Task<IActionResult> UpdateVacancy([FromBody] UpdateVacancyDto vacancyDto)
    {
        var response = await _vacancyService.UpdateVacancyAsync(vacancyDto);
        return Ok(response);
    }

    [HttpDelete("[action]/{id}")]
    public async Task<IActionResult> DeleteVacancy(int id)
    {
        var response = await _vacancyService.DeleteVacancyAsync(id);
        return Ok(response);
    }

    [HttpGet("[action]")]
    [Authorize()]
    public async Task<IActionResult> GetVacancies()
    {
        var response = await _vacancyService.GetVacanciesAsync();
        return Ok(response);
    }

    [HttpPost("[action]")]
    public async Task<IActionResult> SetVacancyStatus([FromBody] SetVacancyStatusDto vacancyStatusDto)
    {
        var response = await _vacancyService.SetVacancyStatusAsync(vacancyStatusDto);
        return Ok(response);
    }
    [HttpPost("[action]")]
    public async Task<IActionResult> FilterVacancies([FromBody] FilterVacancyDto filterDto)
    {
        var result = await _vacancyService.FilterVacanciesAsync(filterDto);
        return Ok(result);
    }
}
