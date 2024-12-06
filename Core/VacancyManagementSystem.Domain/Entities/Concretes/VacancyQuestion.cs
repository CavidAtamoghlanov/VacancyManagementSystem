using VacancyManagementSystem.Domain.Entities.Abstacts;

namespace VacancyManagementSystem.Domain.Entities.Concretes;

public class VacancyQuestion : BaseEntity<int>
{
    public int VacancyId { get; set; }
    public int QuestionBankId { get; set; }  

    // Navigation Properties
    public virtual Vacancy Vacancy { get; set; }
    public virtual QuestionBank QuestionBank { get; set; }
}