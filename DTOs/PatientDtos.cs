using System.ComponentModel.DataAnnotations;

namespace EmergencyPatientMonitoring.API.DTOs;

public class PatientProfileDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = default!;
    public string Email { get; set; } = default!;
    public string? Phone { get; set; }
    public string PatientCode { get; set; } = default!;
    public DateTime DateOfBirth { get; set; }
    public string? BloodType { get; set; }
    public HealthHistoryDto? HealthHistory { get; set; }
}

public class UpdatePatientDto
{
    [Required, MaxLength(150)]
    public string Name { get; set; } = default!;
    [Phone]
    public string? Phone { get; set; }
    [Required]
    public DateTime DateOfBirth { get; set; }
    [MaxLength(5)]
    public string? BloodType { get; set; }
}

public class HealthHistoryDto
{
    public string? ChronicConditions { get; set; }
    public string? Allergies { get; set; }
}

public class AddMonitorDto
{
    [Required, EmailAddress]
    public string MonitorEmail { get; set; } = default!;
    [Required, MaxLength(50)]
    public string RelationshipToPatient { get; set; } = default!;
}

public class MonitorLinkDto
{
    public Guid MonitorId { get; set; }
    public string Name { get; set; } = default!;
    public string Email { get; set; } = default!;
    public string? Phone { get; set; }
    public string RelationshipToPatient { get; set; } = default!;
    public bool SmsVerified { get; set; }
    public bool CallVerified { get; set; }
    public DateTime? VerifiedAt { get; set; }
}

public class MonitoredPatientDto
{
    public Guid PatientId { get; set; }
    public string Name { get; set; } = default!;
    public string PatientCode { get; set; } = default!;
    public string RelationshipToPatient { get; set; } = default!;
}