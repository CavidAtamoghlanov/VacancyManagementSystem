namespace VacancyManagementSystem.Domain.DTOs;
public class AddApplicantDto
{
    public int UserId { get; set; }
    public int VacancyId { get; set; }
    public double TestScore { get; set; }
    public string CVPath { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
}