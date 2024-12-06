namespace VacancyManagementSystem.Domain.DTOs;

public class ResumeTestDto
{
    public int QuestionId { get; set; }
    public string QuestionText { get; set; }
    public List<GetAnswerOptionDto> AnswerOptions { get; set; }
}
