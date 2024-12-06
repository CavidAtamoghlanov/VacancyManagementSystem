using Microsoft.AspNetCore.Mvc;
using VacancyManagementSystem.Application.Services;
using VacancyManagementSystem.Domain.DTOs;

namespace VacancyManagementSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApplicantController : ControllerBase
    {
        private readonly IApplicantService _applicantService;

        public ApplicantController(IApplicantService applicantService)
        {
            _applicantService = applicantService;
        }

        [HttpGet("[action]/{vacancyId}")]
        public async Task<IActionResult> GetAllApplicantsForVacancy(int vacancyId)
        {
            var response = await _applicantService.GetAllApplicantsForVacancyAsync(vacancyId);
            return Ok(response);
        }

        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> GetApplicantById(int id)
        {
            var response = await _applicantService.GetApplicantByIdAsync(id);
            return Ok(response);
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> CreateApplicant([FromBody] AddApplicantDto applicantDto)
        {
            var response = await _applicantService.CreateApplicantAsync(applicantDto);
            return Ok(response);
        }

        [HttpPut("[action]")]
        public async Task<IActionResult> UpdateApplicant([FromBody] UpdateApplicantDto applicantDto)
        {
            var response = await _applicantService.UpdateApplicantAsync(applicantDto);
            return Ok(response);
        }

        [HttpDelete("[action]/{id}")]
        public async Task<IActionResult> DeleteApplicant(int id)
        {
            var response = await _applicantService.DeleteApplicantAsync(id);
            return Ok(response);
        }

        [HttpGet("[action]/{applicantId}")]
        public async Task<IActionResult> GetApplicantTestResults(int applicantId)
        {
            var response = await _applicantService.GetApplicantTestResultsAsync(applicantId);
            return Ok(response);
        }

        [HttpPost("[action]/{applicantId}")]
        public async Task<IActionResult> UploadApplicantCV(int applicantId, [FromForm] IFormFile file)
        {
            var response = await _applicantService.UploadApplicantCVAsync(applicantId, file);
            return Ok(response);
        }
        [HttpPost("[action]")]
        public async Task<IActionResult> SearchApplicants([FromBody] SearchApplicantDto searchDto)
        {
            var response = await _applicantService.SearchApplicantsAsync(searchDto);
            return StatusCode(response.StatusCode, response.Content);
        }
        [HttpGet("[action]/{applicantId}")]
        public async Task<IActionResult> DownloadApplicantCV(int applicantId)
        {
            var response = await _applicantService.DownloadApplicantCvAsync(applicantId);

            if (response.StatusCode != 200)
                return StatusCode(response.StatusCode, response.Content);

            var result = response.Content as dynamic;
            var fileName = result?.FileName;
            var fileContent = Convert.FromBase64String(result?.FileContent);

            return File(fileContent, "application/octet-stream", fileName);
        }

    }
}
