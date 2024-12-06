namespace VacancyManagementSystem.Domain.DTOs;

public class TestQuestionDto
{
    public int VacancyQuestionId { get; set; }
    public string QuestionText { get; set; }
    public List<AnswerOptionDto> AnswerOptions { get; set; }
}