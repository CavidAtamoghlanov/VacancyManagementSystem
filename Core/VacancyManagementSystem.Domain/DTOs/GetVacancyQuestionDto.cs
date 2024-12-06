namespace VacancyManagementSystem.Domain.DTOs;

public class GetVacancyQuestionDto
{
    public int Id { get; set; }
    public int VacancyId { get; set; }
    public int QuestionBankId { get; set; }
}
