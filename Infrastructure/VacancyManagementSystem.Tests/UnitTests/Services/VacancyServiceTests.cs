using VacancyManagementSystem.Application;
using VacancyManagementSystem.Application.Mappings.Abstracts;
using VacancyManagementSystem.Application.Repositories.Commons;
using VacancyManagementSystem.Domain.DTOs;
using VacancyManagementSystem.Domain.Entities.Concretes;
using VacancyManagementSystem.Infrastructure.Services;

namespace VacancyManagementSystem.Tests.UnitTests.Services;

public class VacancyServiceTests
{
    private Mock<IUnitOfWork> _unitOfWorkMock;
    private Mock<IAutoMapperConfiguration> _mapperMock;
    private VacancyService _vacancyService;

    [SetUp]
    public void Setup()
    {
        _unitOfWorkMock = new Mock<IUnitOfWork>();
        _mapperMock = new Mock<IAutoMapperConfiguration>();
        _vacancyService = new VacancyService(_unitOfWorkMock.Object, _mapperMock.Object);
    }

    [Test]
    public async Task DeleteVacancyAsync_VacancyNotFound_ReturnsNotFoundResponse()
    {
        // Arrange
        var vacancyId = 1;
        var repositoryMock = new Mock<IGenericRepository<Vacancy, int>>();
        repositoryMock.Setup(r => r.GetByIdAsync(vacancyId, false)).ReturnsAsync((Vacancy)null);

        _unitOfWorkMock.Setup(u => u.GetRepository<Vacancy, int>()).Returns(repositoryMock.Object);

        // Act
        var response = await _vacancyService.DeleteVacancyAsync(vacancyId);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(response.StatusCode, Is.EqualTo(404));
            Assert.That(response.ExceptionMessage, Is.EqualTo("Vacancy not found."));
        });
        Assert.That(response.Content, Is.Null);
    }

    [Test]
    public async Task SetVacancyStatusAsync_ValidVacancy_UpdatesStatusSuccessfully()
    {
        // Arrange
        var vacancyId = 1;
        var setStatusDto = new SetVacancyStatusDto { Id = vacancyId, IsActive = true };
        var vacancy = new Vacancy { Id = vacancyId, IsActive = false };

        var repositoryMock = new Mock<IGenericRepository<Vacancy, int>>();
        repositoryMock.Setup(r => r.GetByIdAsync(vacancyId, false)).ReturnsAsync(vacancy);

        _unitOfWorkMock.Setup(u => u.GetRepository<Vacancy, int>()).Returns(repositoryMock.Object);

        // Act
        var response = await _vacancyService.SetVacancyStatusAsync(setStatusDto);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(response.StatusCode, Is.EqualTo(200));
            Assert.That(response?.Content?.ToString(), Is.EqualTo("Vacancy active successfully."));
            Assert.That(vacancy.IsActive, Is.True);
        });
        repositoryMock.Verify(r => r.Update(vacancy), Times.Once);
        _unitOfWorkMock.Verify(u => u.Commit(), Times.Once);
    }

    [Test]
    public async Task CreateVacancyAsync_ValidData_CreatesVacancySuccessfully()
    {
        // Arrange
        var vacancyDto = new AddVacancyDto { Title = "Developer" };
        var vacancy = new Vacancy { Title = "Developer" };

        var repositoryMock = new Mock<IGenericRepository<Vacancy, int>>();
        _unitOfWorkMock.Setup(u => u.GetRepository<Vacancy, int>()).Returns(repositoryMock.Object);
        _mapperMock.Setup(m => m.Map<Vacancy, AddVacancyDto>(vacancyDto)).Returns(vacancy);

        // Act
        var response = await _vacancyService.CreateVacancyAsync(vacancyDto);

        // Assert
        Assert.That(response.StatusCode, Is.EqualTo(200));
        Assert.That(response?.Content?.ToString(), Is.EqualTo("Vacancy successfully created."));
        repositoryMock.Verify(r => r.AddAsync(vacancy), Times.Once);
        _unitOfWorkMock.Verify(u => u.Commit(), Times.Once);
    }

    [Test]
    public async Task UpdateVacancyAsync_VacancyNotFound_ReturnsNotFoundResponse()
    {
        // Arrange
        var vacancyDto = new UpdateVacancyDto { Id = 1 };
        var repositoryMock = new Mock<IGenericRepository<Vacancy, int>>();
        repositoryMock.Setup(r => r.GetByIdAsync(vacancyDto.Id, false)).ReturnsAsync((Vacancy)null);

        _unitOfWorkMock.Setup(u => u.GetRepository<Vacancy, int>()).Returns(repositoryMock.Object);

        // Act
        var response = await _vacancyService.UpdateVacancyAsync(vacancyDto);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(response.StatusCode, Is.EqualTo(404));
            Assert.That(response.ExceptionMessage, Is.EqualTo("Vacancy not found."));
        });
    }
}