namespace VacancyManagementSystem.Domain.DTOs;

public class GetTestAnswerDto
{
    public int QuestionId { get; set; }
    public string QuestionText { get; set; }
    public string AnswerOptionText { get; set; }
    public bool IsCorrect { get; set; }
}
