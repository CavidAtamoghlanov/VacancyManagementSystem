namespace VacancyManagementSystem.Application.Mappings.Abstracts;

public interface IAutoMapperConfiguration
{
    public IEnumerable<T> Map<T, TModel>(IEnumerable<TModel> model) where T : class, new();
    public T Map<T, TModel>(TModel model) where T : class, new();
}