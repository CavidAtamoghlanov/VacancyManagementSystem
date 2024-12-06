namespace VacancyManagementSystem.Domain.DTOs;

public class SearchApplicantDto
{
    public string? FullName { get; set; }
    public string? Email { get; set; }
    public double? MinTestScore { get; set; }
    public double? MaxTestScore { get; set; }
    public bool? IsDeleted { get; set; }
}
