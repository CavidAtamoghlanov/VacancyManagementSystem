namespace VacancyManagementSystem.Domain.DTOs;

public class UpdateApplicantDto
{
    public int Id { get; set; }
    public double TestScore { get; set; }
    public string CVPath { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public bool IsDeleted { get; set; }
}