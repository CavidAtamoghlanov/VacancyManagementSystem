namespace VacancyManagementSystem.Domain.DTOs;

public class ChangePasswordDto
{
    public int UserId { get; set; }
    public string Email { get; set; }
    public string OldPassword { get; set; }
    public string NewPassword { get; set; }
}
