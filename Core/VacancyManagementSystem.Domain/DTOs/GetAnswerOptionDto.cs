namespace VacancyManagementSystem.Domain.DTOs;

public class GetAnswerOptionDto
{
    public int Id { get; set; }
    public string OptionText { get; set; }
    public bool IsCorrect { get; set; }
}
