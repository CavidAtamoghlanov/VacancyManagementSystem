namespace VacancyManagementSystem.Domain.DTOs;

public class UpdateAnswerOptionDto
{
    public int Id { get; set; }
    public string OptionText { get; set; }
    public bool IsCorrect { get; set; }
    public int QuestionBankId { get; set; }
}
