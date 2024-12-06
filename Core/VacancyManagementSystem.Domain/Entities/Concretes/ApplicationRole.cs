using Microsoft.AspNetCore.Identity;
using VacancyManagementSystem.Domain.Entities.Abstacts;

namespace VacancyManagementSystem.Domain.Entities.Concretes;

public class ApplicationRole : IdentityRole<int>, IBaseEntity<int>
{
    public string Role { get; set; }
    public bool IsDeleted { get; set; }
    public DateTime CreatedDate { get; set; } = DateTime.Now;
    public DateTime? ModifiedDate { get; set; }

    // Navigation Properties
    public virtual ICollection<ApplicationUser> Users { get; set; }
}