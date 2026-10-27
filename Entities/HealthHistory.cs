namespace EmergencyPatientMonitoring.API.Entities;

public class HealthHistory
{
    public Guid Id { get; set; }
    public Guid PatientId { get; set; }
    public Patient Patient { get; set; } = default!;

    public string? ChronicConditions { get; set; }
    public string? Allergies { get; set; }
}