namespace VacancyManagementSystem.Domain.DTOs;

public class FilterVacancyDto
{
    public string Title { get; set; }
    public string Category { get; set; }
    public bool? IsActive { get; set; }
    public DateTime? CreatedAfter { get; set; }
    public DateTime? CreatedBefore { get; set; }
}