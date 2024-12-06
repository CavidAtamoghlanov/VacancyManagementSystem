using VacancyManagementSystem.Application.Responses;
using VacancyManagementSystem.Domain.DTOs;

namespace VacancyManagementSystem.Application.Services;

public interface IAuthService
{
    Task<Response> RegisterAsync(RegisterDto registerDto);
    Task<Response> LoginAsync(LoginDto loginDto);
    Task<Response> ChangePasswordAsync(ChangePasswordDto changePasswordDto);
    Task<Response> ForgotPasswordAsync(ForgotPasswordDto forgotPasswordDto);
    Task<Response> ResetPasswordAsync(ResetPasswordDto resetPasswordDto);
}