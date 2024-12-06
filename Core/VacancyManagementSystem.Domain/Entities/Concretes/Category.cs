using VacancyManagementSystem.Domain.Entities.Abstacts;

namespace VacancyManagementSystem.Domain.Entities.Concretes;

public class Category : BaseEntity<int>
{
    public string Name { get; set; }
    public string Description { get; set; }

    // Navigation Properties
    public virtual ICollection<Vacancy> Vacancies { get; set; }
    public virtual ICollection<QuestionBank> QuestionBanks { get; set; }
}