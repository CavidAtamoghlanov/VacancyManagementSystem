using VacancyManagementSystem.Domain.Entities.Abstacts;

namespace VacancyManagementSystem.Domain.Entities.Concretes;

public class QuestionBank : BaseEntity<int>
{
    public string QuestionText { get; set; }  
    public int CategoryId { get; set; }  

    // Navigation Properties
    public virtual Category Category { get; set; }
    public virtual ICollection<AnswerOption> AnswerOptions { get; set; }  
}