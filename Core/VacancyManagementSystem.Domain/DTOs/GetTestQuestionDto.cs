namespace VacancyManagementSystem.Domain.DTOs;

public class GetTestQuestionDto
{
    public int Id { get; set; }
    public string QuestionText { get; set; }
    public List<GetAnswerOptionDto> AnswerOptions { get; set; }
}
