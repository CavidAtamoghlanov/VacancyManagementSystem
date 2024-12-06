namespace VacancyManagementSystem.Domain.DTOs;

public class GetApplicantDto
{
    public int Id { get; set; }
    public string UserId { get; set; }
    public int VacancyId { get; set; }
    public double TestScore { get; set; }
    public string CVPath { get; set; }
}