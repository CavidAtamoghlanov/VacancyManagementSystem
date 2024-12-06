using VacancyManagementSystem.Application;
using VacancyManagementSystem.Application.Mappings.Abstracts;
using VacancyManagementSystem.Application.Responses;
using VacancyManagementSystem.Application.Services;
using VacancyManagementSystem.Domain.DTOs;
using VacancyManagementSystem.Domain.Entities.Concretes;
using VacancyManagementSystem.Infrastructure.Services.Commons;

namespace VacancyManagementSystem.Infrastructure.Services
{
    public class VacancyService : BaseService, IVacancyService
    {
        public VacancyService(IUnitOfWork unitOfWork, IAutoMapperConfiguration autoMapper) : base(unitOfWork, autoMapper)
        {
        }

        public async Task<Response> CreateVacancyAsync(AddVacancyDto vacancyDto)
        {
            var vacancy = _autoMapper.Map<Vacancy, AddVacancyDto>(vacancyDto);

            var vacancyRepository = _unitOfWork.GetRepository<Vacancy, int>();
            await vacancyRepository.AddAsync(vacancy);
            await _unitOfWork.Commit();
            return Success("Vacancy successfully created.");
        }

        public async Task<Response> UpdateVacancyAsync(UpdateVacancyDto vacancyDto)
        {
            var vacancyRepository = _unitOfWork.GetRepository<Vacancy, int>();
            var vacancy = await vacancyRepository.GetByIdAsync(vacancyDto.Id);

            if (vacancy == null)
                return NotFound("Vacancy not found.");

            vacancy = _autoMapper.Map<Vacancy, UpdateVacancyDto>(vacancyDto);

            vacancyRepository.Update(vacancy);
            await _unitOfWork.Commit();
            return Success("Vacancy successfully updated.");
        }

        public async Task<Response> DeleteVacancyAsync(int id)
        {
            var vacancyRepository = _unitOfWork.GetRepository<Vacancy, int>();
            var vacancy = await vacancyRepository.GetByIdAsync(id);

            if (vacancy == null)
                return NotFound("Vacancy not found.");

            vacancyRepository.Delete(vacancy);
            await _unitOfWork.Commit();
            return Success("Vacancy successfully deleted.");
        }

        public async Task<Response> GetVacanciesAsync()
        {
            var vacancyRepository = _unitOfWork.GetRepository<Vacancy, int>();
            var vacancies = (await vacancyRepository.GetAllAsync()).ToList();

            if (vacancies == null || !vacancies.Any())
                return NotFound("No vacancies found.");

            var vacancyDtos = _autoMapper.Map<GetVacancyDto, Vacancy>(vacancies);
            return Success(vacancyDtos);
        }

        public async Task<Response> SetVacancyStatusAsync(SetVacancyStatusDto vacancyStatusDto)
        {
            var vacancyRepository = _unitOfWork.GetRepository<Vacancy, int>();
            var vacancy = await vacancyRepository.GetByIdAsync(vacancyStatusDto.Id);

            if (vacancy == null)
                return NotFound("No vacancies found.");

            vacancy.IsActive = vacancyStatusDto.IsActive;
            vacancy.ModifiedDate = DateTime.Now;
            vacancyRepository.Update(vacancy);
            await _unitOfWork.Commit();

            var status = vacancyStatusDto.IsActive ? "active" : "inactive";
            return Success($"Vacancy {status} successfully.");
        }
        public async Task<Response> FilterVacanciesAsync(FilterVacancyDto filterDto)
        {
            var vacancyRepository = _unitOfWork.GetRepository<Vacancy, int>();
            var query = (await vacancyRepository.GetAllAsync()).AsQueryable();

            if (!string.IsNullOrWhiteSpace(filterDto.Title))
                query = query.Where(v => v.Title.Contains(filterDto.Title));

            if (!string.IsNullOrWhiteSpace(filterDto.Category))
                query = query.Where(v => v.Category.Name == filterDto.Category);

            if (filterDto.IsActive.HasValue)
                query = query.Where(v => v.IsActive == filterDto.IsActive);

            if (filterDto.CreatedAfter.HasValue)
                query = query.Where(v => v.CreatedDate >= filterDto.CreatedAfter);

            if (filterDto.CreatedBefore.HasValue)
                query = query.Where(v => v.CreatedDate <= filterDto.CreatedBefore);

            var filteredVacancies = query.ToList();

            if (!filteredVacancies.Any())
                return NotFound("No vacancies match the filter criteria.");

            var vacancyDtos = _autoMapper.Map<List<GetVacancyDto>, List<Vacancy>>(filteredVacancies);

            return Success(vacancyDtos);
        }

    }
}
