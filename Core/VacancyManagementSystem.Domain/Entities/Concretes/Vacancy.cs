using VacancyManagementSystem.Domain.Entities.Abstacts;

namespace VacancyManagementSystem.Domain.Entities.Concretes;

public class Vacancy : BaseEntity<int>
{
    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public bool IsActive { get; set; }
    public int QuestionCount { get; set; } 

    // Foreign Keys
    public int CategoryId { get; set; }

    // Navigation Properties
    public virtual Category Category { get; set; }
    public virtual ICollection<VacancyQuestion> VacancyQuestions { get; set; }
    public virtual ICollection<Applicant> Applicants { get; set; }
}