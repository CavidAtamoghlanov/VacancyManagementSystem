namespace VacancyManagementSystem.Domain.DTOs;

public class GetApplicantsByVacancyDto
{
    public int VacancyId { get; set; }
    public List<GetApplicantDto> Applicants { get; set; }
}