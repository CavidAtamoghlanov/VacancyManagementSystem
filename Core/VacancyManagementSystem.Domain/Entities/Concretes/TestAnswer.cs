using VacancyManagementSystem.Domain.Entities.Abstacts;

namespace VacancyManagementSystem.Domain.Entities.Concretes;

public class TestAnswer : BaseEntity<int>
{
    public int ApplicantId { get; set; }  
    public int VacancyQuestionId { get; set; }  
    public int AnswerOptionId { get; set; } 
    public bool IsCorrect { get; set; }  

    // Navigation Properties
    public virtual Applicant Applicant { get; set; }
    public virtual VacancyQuestion VacancyQuestion { get; set; }
    public virtual AnswerOption AnswerOption { get; set; } 
}