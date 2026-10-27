// Services/AuthService.cs
using EmergencyPatientMonitoring.API.Data;
using EmergencyPatientMonitoring.API.DTOs;
using EmergencyPatientMonitoring.API.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Monitor = EmergencyPatientMonitoring.API.Entities.Monitor;

namespace EmergencyPatientMonitoring.API.Services;

public class AuthService : IAuthService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<IdentityRole<Guid>> _roleManager;
    private readonly ITokenService _tokenService;
    private readonly AppDbContext _db;

    public AuthService(
        UserManager<ApplicationUser> userManager,
        RoleManager<IdentityRole<Guid>> roleManager,
        ITokenService tokenService,
        AppDbContext db)
    {
        _userManager = userManager;
        _roleManager = roleManager;
        _tokenService = tokenService;
        _db = db;
    }

    public async Task<(AuthResponseDto? Result, IEnumerable<string> Errors)> RegisterAsync(RegisterDto dto)
    {
        if (dto.Role == UserRole.Patient && dto.DateOfBirth is null)
            return (null, new[] { "DateOfBirth is required for patients." });

        if (dto.Role == UserRole.Admin)
            return (null, new[] { "Admin accounts cannot be self-registered." });

        var user = new ApplicationUser
        {
            Id = Guid.NewGuid(),
            UserName = dto.Email,
            Email = dto.Email,
            PhoneNumber = dto.Phone,
            Name = dto.Name
        };

        await using var tx = await _db.Database.BeginTransactionAsync();

        var createResult = await _userManager.CreateAsync(user, dto.Password);
        if (!createResult.Succeeded)
            return (null, createResult.Errors.Select(e => e.Description));

        var roleName = dto.Role.ToString();
        await _userManager.AddToRoleAsync(user, roleName);

        string? patientCode = null;

        if (dto.Role == UserRole.Patient)
        {
            patientCode = $"PT-{Guid.NewGuid().ToString("N")[..8].ToUpper()}";
            _db.Patients.Add(new Patient
            {
                Id = Guid.NewGuid(),
                UserId = user.Id,
                PatientCode = patientCode,
                DateOfBirth = dto.DateOfBirth!.Value,
                BloodType = dto.BloodType
            });
        }
        else if (dto.Role == UserRole.Monitor)
        {
            _db.Monitors.Add(new Monitor { Id = Guid.NewGuid(), UserId = user.Id });
        }

        await _db.SaveChangesAsync();
        await tx.CommitAsync();

        var (token, expires) = _tokenService.CreateToken(user, roleName);

        return (new AuthResponseDto
        {
            UserId = user.Id,
            Name = user.Name,
            Email = user.Email!,
            Role = roleName,
            Token = token,
            ExpiresAt = expires,
            PatientCode = patientCode
        }, Array.Empty<string>());
    }

    public async Task<AuthResponseDto?> LoginAsync(LoginDto dto)
    {
        var user = await _userManager.FindByEmailAsync(dto.Email);
        if (user is null || user.AccountStatus != "Active") return null;

        if (!await _userManager.CheckPasswordAsync(user, dto.Password)) return null;

        var role = (await _userManager.GetRolesAsync(user)).FirstOrDefault() ?? "Patient";
        var (token, expires) = _tokenService.CreateToken(user, role);

        string? patientCode = null;
        if (role == "Patient")
        {
            patientCode = await _db.Patients
                .Where(p => p.UserId == user.Id)
                .Select(p => p.PatientCode)
                .FirstOrDefaultAsync();
        }

        return new AuthResponseDto
        {
            UserId = user.Id,
            Name = user.Name,
            Email = user.Email!,
            Role = role,
            Token = token,
            ExpiresAt = expires,
            PatientCode = patientCode
        };
    }
}