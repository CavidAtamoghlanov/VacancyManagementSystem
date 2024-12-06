namespace VacancyManagementSystem.Domain.DTOs;

public class AnswerOptionDto
{
    public string OptionText { get; set; }
    public bool IsCorrect { get; set; }
    public int QuestionBankId { get; set; }  
}