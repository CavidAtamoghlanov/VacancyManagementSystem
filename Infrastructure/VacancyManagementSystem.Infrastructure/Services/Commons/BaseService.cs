using AutoMapper;
using VacancyManagementSystem.Application;
using VacancyManagementSystem.Application.Mappings.Abstracts;

namespace VacancyManagementSystem.Infrastructure.Services.Commons;


public abstract class BaseService : ResponceMethods
{
    protected readonly IUnitOfWork _unitOfWork;
    protected readonly IAutoMapperConfiguration _autoMapper;

    public BaseService(IUnitOfWork unitOfWork, IAutoMapperConfiguration autoMapper)
    {
        _unitOfWork = unitOfWork;
        _autoMapper = autoMapper;
    }
}