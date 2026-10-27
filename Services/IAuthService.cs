// Services/IAuthService.cs
using EmergencyPatientMonitoring.API.DTOs;

namespace EmergencyPatientMonitoring.API.Services;

public interface IAuthService
{
    Task<(AuthResponseDto? Result, IEnumerable<string> Errors)> RegisterAsync(RegisterDto dto);
    Task<AuthResponseDto?> LoginAsync(LoginDto dto);
}