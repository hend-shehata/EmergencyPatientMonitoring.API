using System.ComponentModel.DataAnnotations;

namespace EmergencyPatientMonitoring.API.DTOs;

public enum UserRole { Patient, Monitor, Admin }

public class RegisterDto
{
    [Required, MaxLength(150)]
    public string Name { get; set; } = default!;

    [Required, EmailAddress]
    public string Email { get; set; } = default!;

    [Required, Phone]
    public string Phone { get; set; } = default!;

    [Required, MinLength(8)]
    public string Password { get; set; } = default!;

    [Required]
    public UserRole Role { get; set; }

    // للمريض فقط
    public DateTime? DateOfBirth { get; set; }
    public string? BloodType { get; set; }
}

public class LoginDto
{
    [Required, EmailAddress]
    public string Email { get; set; } = default!;

    [Required]
    public string Password { get; set; } = default!;
}

public class AuthResponseDto
{
    public Guid UserId { get; set; }
    public string Name { get; set; } = default!;
    public string Email { get; set; } = default!;
    public string Role { get; set; } = default!;
    public string Token { get; set; } = default!;
    public DateTime ExpiresAt { get; set; }
    public string? PatientCode { get; set; }
}
