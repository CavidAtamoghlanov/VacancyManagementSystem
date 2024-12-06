using Microsoft.AspNetCore.Http;
using VacancyManagementSystem.Application.Mappings.Abstracts;
using VacancyManagementSystem.Application;
using VacancyManagementSystem.Application.Responses;
using VacancyManagementSystem.Application.Services;
using VacancyManagementSystem.Domain.DTOs;
using VacancyManagementSystem.Domain.Entities.Concretes;
using VacancyManagementSystem.Infrastructure.Services.Commons;

namespace VacancyManagementSystem.Infrastructure.Services;

public class ApplicantService : BaseService, IApplicantService
{
    public ApplicantService(IUnitOfWork unitOfWork, IAutoMapperConfiguration autoMapper)
        : base(unitOfWork, autoMapper)
    {
    }

    public async Task<Response> GetAllApplicantsForVacancyAsync(int vacancyId)
    {
        var applicantRepository = _unitOfWork.GetRepository<Applicant, int>();
        var applicants = await applicantRepository.GetAllAsync(a => a.VacancyId == vacancyId);

        if (applicants == null || !applicants.Any())
            return NotFound("No applicants found for this vacancy.");

        var applicantDtos = _autoMapper.Map<List<GetApplicantDto>, IEnumerable<Applicant>>(applicants);
        return Success(applicantDtos);
    }

    public async Task<Response> GetApplicantByIdAsync(int id)
    {
        var applicantRepository = _unitOfWork.GetRepository<Applicant, int>();
        var applicant = await applicantRepository.GetByIdAsync(id);

        if (applicant == null)
            return NotFound("Applicant not found.");

        var applicantDto = _autoMapper.Map<GetApplicantDto, Applicant>(applicant);
        return Success(applicantDto);
    }

    public async Task<Response> CreateApplicantAsync(AddApplicantDto applicantDto)
    {
        var applicant = _autoMapper.Map<Applicant, AddApplicantDto>(applicantDto);
        var applicantRepository = _unitOfWork.GetRepository<Applicant, int>();
        await applicantRepository.AddAsync(applicant);
        await _unitOfWork.Commit();
        return Success("Applicant successfully created.");
    }

    public async Task<Response> UpdateApplicantAsync(UpdateApplicantDto applicantDto)
    {
        var applicantRepository = _unitOfWork.GetRepository<Applicant, int>();
        var applicant = await applicantRepository.GetByIdAsync(applicantDto.Id);

        if (applicant == null)
            return NotFound("Applicant not found.");

        applicant = _autoMapper.Map<Applicant, UpdateApplicantDto>(applicantDto);
        applicantRepository.Update(applicant);
        await _unitOfWork.Commit();
        return Success("Applicant successfully updated.");
    }

    public async Task<Response> DeleteApplicantAsync(int id)
    {
        var applicantRepository = _unitOfWork.GetRepository<Applicant, int>();
        var applicant = await applicantRepository.GetByIdAsync(id);

        if (applicant == null)
            return NotFound("Applicant not found.");

        applicant.IsDeleted = true;
        applicantRepository.Update(applicant);
        await _unitOfWork.Commit();
        return Success("Applicant successfully deleted.");
    }

    public async Task<Response> GetApplicantTestResultsAsync(int applicantId)
    {
        var applicantRepository = _unitOfWork.GetRepository<Applicant, int>();
        var applicant = await applicantRepository.GetByIdAsync(applicantId);

        if (applicant == null)
            return NotFound("Applicant not found.");

        return Success(new GetApplicantTestResultsDto
        {
            ApplicantId = applicantId,
            TestScore = applicant.TestScore
        });
    }

    public async Task<Response> UploadApplicantCVAsync(int applicantId, IFormFile file)
    {
        var applicantRepository = _unitOfWork.GetRepository<Applicant, int>();
        var applicant = await applicantRepository.GetByIdAsync(applicantId);

        if (applicant == null)
            return NotFound("Applicant not found.");

        var fileExtension = Path.GetExtension(file.FileName);
        var uniqueFileName = Guid.NewGuid().ToString() + fileExtension;


        var directoryPath = Path.Combine(Directory.GetCurrentDirectory(), "Resources");
        if (!Directory.Exists(directoryPath))
            Directory.CreateDirectory(directoryPath);


        var filePath = Path.Combine(directoryPath, uniqueFileName);


        using var stream = new FileStream(filePath, FileMode.Create);
        await file.CopyToAsync(stream);



        applicant.CVPath = $"{uniqueFileName}";
        applicantRepository.Update(applicant);
        await _unitOfWork.Commit();

        return Success("Applicant CV uploaded successfully.");
    }
    public async Task<Response> SearchApplicantsAsync(SearchApplicantDto searchDto)
    {
        var applicantRepository = _unitOfWork.GetRepository<Applicant, int>();
        var query = (await applicantRepository.GetAllAsync()).AsQueryable();

        if (!string.IsNullOrWhiteSpace(searchDto.FullName))
            query = query.Where(a => a.FullName.Contains(searchDto.FullName));

        if (!string.IsNullOrWhiteSpace(searchDto.Email))
            query = query.Where(a => a.Email == searchDto.Email);

        if (searchDto.MinTestScore.HasValue)
            query = query.Where(a => a.TestScore >= searchDto.MinTestScore);

        if (searchDto.MaxTestScore.HasValue)
            query = query.Where(a => a.TestScore <= searchDto.MaxTestScore);

        if (searchDto.IsDeleted.HasValue)
            query = query.Where(a => a.IsDeleted == searchDto.IsDeleted);

        var applicants = query.ToList();

        if (!applicants.Any())
            return NotFound("No applicants match the search criteria.");

        var applicantDtos = _autoMapper.Map<List<GetApplicantDto>, List<Applicant>>(applicants);

        return Success(applicantDtos);
    }

    public async Task<Response> DownloadApplicantCvAsync(int applicantId)
    {
        var applicantRepository = _unitOfWork.GetRepository<Applicant, int>();
        var applicant = await applicantRepository.GetByIdAsync(applicantId);

        if (applicant == null)
            return NotFound("Applicant Not found.");

        var directoryPath = Path.Combine(Directory.GetCurrentDirectory(), "Resources");
        var filePath = Path.Combine(directoryPath, applicant.CVPath);

        if (string.IsNullOrWhiteSpace(applicant.CVPath) || !File.Exists(filePath))
            return NotFound("CV not found.");

        var fileBytes = await File.ReadAllBytesAsync(filePath);
        var fileName = Path.GetFileName(filePath);

        return Success(new { FileName = fileName, FileContent = Convert.ToBase64String(fileBytes) });
    }
}
