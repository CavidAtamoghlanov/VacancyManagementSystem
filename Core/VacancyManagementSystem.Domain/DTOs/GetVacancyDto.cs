namespace VacancyManagementSystem.Domain.DTOs;

public class GetVacancyDto
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public bool IsActive { get; set; }

    public string CategoryName { get; set; }
}
