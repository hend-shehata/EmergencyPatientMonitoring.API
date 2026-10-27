namespace EmergencyPatientMonitoring.API.Entities;

public class Monitor
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public ApplicationUser User { get; set; } = default!;

    public ICollection<PatientMonitor> PatientMonitors { get; set; } = new List<PatientMonitor>();
    public ICollection<Alert> AcknowledgedAlerts { get; set; } = new List<Alert>();
}