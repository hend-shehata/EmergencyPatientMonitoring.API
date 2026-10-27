// Services/ITokenService.cs
using EmergencyPatientMonitoring.API.Entities;

namespace EmergencyPatientMonitoring.API.Services;

public interface ITokenService
{
    (string Token, DateTime ExpiresAt) CreateToken(ApplicationUser user, string role);
}