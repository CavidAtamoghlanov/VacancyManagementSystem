namespace VacancyManagementSystem.Domain.Entities.Abstacts;

public interface IBaseEntity<TKey>
{
    TKey Id { get; set; }
    bool IsDeleted { get; set; }
    DateTime CreatedDate { get; set; }
    DateTime? ModifiedDate { get; set; }
}
