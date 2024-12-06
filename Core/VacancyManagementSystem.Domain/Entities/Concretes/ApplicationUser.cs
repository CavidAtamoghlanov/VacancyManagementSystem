using Microsoft.AspNetCore.Identity;
using VacancyManagementSystem.Domain.Entities.Abstacts;

namespace VacancyManagementSystem.Domain.Entities.Concretes;

public class ApplicationUser : IdentityUser<int>, IBaseEntity<int>
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string PhoneNumber { get; set; }
    public string FullName => $"{FirstName} {LastName}";
    public bool IsDeleted { get; set; }
    public DateTime CreatedDate { get; set; } = DateTime.Now;
    public DateTime? ModifiedDate { get; set; }

    // Navigation Properties
    public virtual ICollection<ApplicationRole> Roles { get; set; }
}