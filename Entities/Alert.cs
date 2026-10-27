namespace EmergencyPatientMonitoring.API.Entities;

public class Alert
{
    public Guid Id { get; set; }

    public Guid PredictionId { get; set; }
    public Prediction Prediction { get; set; } = default!;

    public Guid PatientId { get; set; }
    public Patient Patient { get; set; } = default!;

    public Guid? AcknowledgedByMonitorId { get; set; }
    public Monitor? AcknowledgedByMonitor { get; set; }

    public Guid? EmergencyServiceId { get; set; }
    public EmergencyService? EmergencyService { get; set; }

    public string Severity { get; set; } = default!;
    public string Status { get; set; } = "Open";
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? AcknowledgedAt { get; set; }
    public DateTime? ResolvedAt { get; set; }
}