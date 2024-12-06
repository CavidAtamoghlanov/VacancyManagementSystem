
using VacancyManagementSystem.Domain.Entities.Abstacts;

namespace VacancyManagementSystem.Domain.Entities.Concretes;

public class Applicant : BaseEntity<int>
{
    public double TestScore { get; set; }  
    public string CVPath { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public string FullName => $"{FirstName} {LastName}";

    // Foreign Keys
    public int UserId { get; set; }
    public int VacancyId { get; set; }

    // Navigation Properties
    public virtual ApplicationUser User { get; set; }
    public virtual Vacancy Vacancy { get; set; }
    public virtual ICollection<TestAnswer> TestAnswers { get; set; } 
}