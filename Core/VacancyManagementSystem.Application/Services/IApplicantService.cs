using VacancyManagementSystem.Application.Responses;
using VacancyManagementSystem.Domain.DTOs;
using Microsoft.AspNetCore.Http;

namespace VacancyManagementSystem.Application.Services;

public interface IApplicantService
{
    Task<Response> GetAllApplicantsForVacancyAsync(int vacancyId);
    Task<Response> GetApplicantByIdAsync(int id);
    Task<Response> CreateApplicantAsync(AddApplicantDto applicantDto);
    Task<Response> UpdateApplicantAsync(UpdateApplicantDto applicantDto);
    Task<Response> DeleteApplicantAsync(int id);
    Task<Response> GetApplicantTestResultsAsync(int applicantId);
    Task<Response> UploadApplicantCVAsync(int applicantId, IFormFile file);
    Task<Response> SearchApplicantsAsync(SearchApplicantDto searchDto);
    Task<Response> DownloadApplicantCvAsync(int applicantId);
}