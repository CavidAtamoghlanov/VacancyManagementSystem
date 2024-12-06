namespace VacancyManagementSystem.Domain.DTOs;

public class VacancyQuestionWithDetailsDto
{
    public int VacancyId { get; set; }
    public int QuestionBankId { get; set; }
    public string QuestionText { get; set; }  
    public string AnswerOptions { get; set; }  
}