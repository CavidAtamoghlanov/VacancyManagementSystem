namespace VacancyManagementSystem.Domain.DTOs;

public class TestAnswerDto
{
    public int ApplicantId { get; set; }
    public int VacancyQuestionId { get; set; }
    public int AnswerOptionId { get; set; }
    public bool IsCorrect { get; set; }
}