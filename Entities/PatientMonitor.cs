namespace EmergencyPatientMonitoring.API.Entities;

public class PatientMonitor
{
    public Guid PatientId { get; set; }
    public Patient Patient { get; set; } = default!;

    public Guid MonitorId { get; set; }
    public Monitor Monitor { get; set; } = default!;

    public string RelationshipToPatient { get; set; } = default!;
    public bool SmsVerified { get; set; }
    public bool CallVerified { get; set; }
    public DateTime? VerifiedAt { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}