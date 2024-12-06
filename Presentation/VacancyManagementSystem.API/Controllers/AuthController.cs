using Microsoft.AspNetCore.Mvc;
using VacancyManagementSystem.Application.Services;
using VacancyManagementSystem.Domain.DTOs;

namespace VacancyManagementSystem.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("[action]")]
    public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
    {
        var response = await _authService.RegisterAsync(registerDto);
        return Ok(response);
    }

    [HttpPost("[action]")]
    public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
    {
        var response = await _authService.LoginAsync(loginDto);
        return Ok(response);
    }

    [HttpPost("[action]")]
    public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordDto changePasswordDto)
    {
        var response = await _authService.ChangePasswordAsync(changePasswordDto);
        return Ok(response);
    }

    [HttpPost("[action]")]
    public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordDto forgotPasswordDto)
    {
        var response = await _authService.ForgotPasswordAsync(forgotPasswordDto);
        return Ok(response);
    }

    [HttpPost("[action]")]
    public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordDto resetPasswordDto)
    {
        var response = await _authService.ResetPasswordAsync(resetPasswordDto);
        return Ok(response);
    }

    [HttpGet("[action]")]
    public IActionResult AccessDenied()
    {
        return Forbid("You do not have access to this resource.");
    }
}