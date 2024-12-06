using VacancyManagementSystem.Domain.Entities.Abstacts;

namespace VacancyManagementSystem.Domain.Entities.Concretes;

public class AnswerOption : BaseEntity<int>
{
    public string OptionText { get; set; }
    public bool IsCorrect { get; set; } 

    // Foreign Keys
    public int QuestionBankId { get; set; }

    // Navigation Properties
    public virtual QuestionBank QuestionBank { get; set; }
}
