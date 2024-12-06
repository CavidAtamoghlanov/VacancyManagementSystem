namespace VacancyManagementSystem.Domain.DTOs;

public class AddVacancyDto
{
    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public bool IsActive { get; set; }
    public int CategoryId { get; set; }
    public int QuestionCount { get; set; }
}