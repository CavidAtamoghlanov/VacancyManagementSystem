using Microsoft.AspNetCore.Identity;
using VacancyManagementSystem.Application;
using VacancyManagementSystem.Application.Mappings.Abstracts;
using VacancyManagementSystem.Application.Responses;
using VacancyManagementSystem.Application.Services;
using VacancyManagementSystem.Domain.DTOs;
using VacancyManagementSystem.Domain.Entities.Concretes;
using VacancyManagementSystem.Infrastructure.Services.Commons;

namespace VacancyManagementSystem.Infrastructure.Services;

public class AuthService : BaseService, IAuthService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly RoleManager<ApplicationRole> _roleManager;
    private readonly ITokenService _tokenService;

    public AuthService(
        IUnitOfWork unitOfWork,
        IAutoMapperConfiguration autoMapper,
        UserManager<ApplicationUser> userManager,
        SignInManager<ApplicationUser> signInManager,
        ITokenService tokenService,
        RoleManager<ApplicationRole> roleManager)
        : base(unitOfWork, autoMapper)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _tokenService = tokenService;
        _roleManager = roleManager;
    }


    public async Task<Response> RegisterAsync(RegisterDto registerDto)
    {
        if (await _userManager.FindByEmailAsync(registerDto.Email) != null)
            return Conflict("Email is already in use.");

        if (registerDto.Password != registerDto.ComfirmPassword)
            return BadRequest("Password and confirm password must be the same.");

        var user = CreateUser(registerDto);

        var result = await _userManager.CreateAsync(user, registerDto.Password);
        if (!result.Succeeded)
            return Error("Registration failed.", GetErrors(result.Errors));

        await EnsureRoleExistsAsync(registerDto.Role);

        await _userManager.AddToRoleAsync(user, registerDto.Role);

        return Success("User registered successfully.");
    }

    private ApplicationUser CreateUser(RegisterDto registerDto)
    {
        return new ApplicationUser
        {
            UserName = registerDto.Email,
            Email = registerDto.Email,
            FirstName = registerDto.FirstName,
            LastName = registerDto.LastName,
            PhoneNumber = registerDto.PhoneNumber
        };
    }

    private async Task EnsureRoleExistsAsync(string roleName)
    {
        if (!await _roleManager.RoleExistsAsync(roleName))
        {
            var role = new ApplicationRole(roleName);
            await _roleManager.CreateAsync(role);
        }
    }

    private string GetErrors(IEnumerable<IdentityError> errors)
    {
        return string.Join(", ", errors.Select(e => e.Description));
    }

    public async Task<Response> LoginAsync(LoginDto loginDto)
    {
        var user = await _userManager.FindByEmailAsync(loginDto.Email);
        if (user == null)
            return Unauthorized("Invalid email or password.");

        var isPasswordValid = await _userManager.CheckPasswordAsync(user, loginDto.Password);
        if (!isPasswordValid)
            return Unauthorized("Invalid email or password.");

        var accessToken = _tokenService.GenerateAccessToken(user);
        return Success(accessToken);
    }
    public async Task<Response> ChangePasswordAsync(ChangePasswordDto changePasswordDto)
    {
        var user = await _userManager.FindByEmailAsync(changePasswordDto.Email);
        if (user == null)
            return NotFound("User not found.");

        var result = await _userManager.ChangePasswordAsync(user, changePasswordDto.OldPassword, changePasswordDto.NewPassword);
        if (!result.Succeeded)
            return Error("Password change failed.", string.Join(", ", result.Errors.Select(e => e.Description)));

        return Success("Password changed successfully.");
    }
    public async Task<Response> ForgotPasswordAsync(ForgotPasswordDto forgotPasswordDto)
    {
        var user = await _userManager.FindByEmailAsync(forgotPasswordDto.Email);
        if (user == null)
            return NotFound("User not found.");

        var token = await _userManager.GeneratePasswordResetTokenAsync(user);

        return Success("Password reset token has been sent.");
    }
    public async Task<Response> ResetPasswordAsync(ResetPasswordDto resetPasswordDto)
    {
        if (resetPasswordDto.Password != resetPasswordDto.ComfirmPassword)
        {
            return BadRequest("Passwords do not match.");
        }

        var user = await _userManager.FindByEmailAsync(resetPasswordDto.Email);
        if (user == null)
        {
            return NotFound("User not found.");
        }

        var result = await _userManager.ResetPasswordAsync(user, resetPasswordDto.Token, resetPasswordDto.Password);
        if (!result.Succeeded)
        {
            return BadRequest("Password reset failed.", string.Join(", ", result.Errors.Select(e => e.Description)));
        }

        return Success("Password reset successfully.");
    }
}
