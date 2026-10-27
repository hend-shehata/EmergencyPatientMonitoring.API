namespace EmergencyPatientMonitoring.API.Entities;

public class Patient
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public ApplicationUser User { get; set; } = default!;

    public string PatientCode { get; set; } = default!;
    public DateTime DateOfBirth { get; set; }
    public string? BloodType { get; set; }

    public HealthHistory? HealthHistory { get; set; }
    public MobileApp? MobileApp { get; set; }
    public ICollection<Device> Devices { get; set; } = new List<Device>();
    public ICollection<SensorReading> SensorReadings { get; set; } = new List<SensorReading>();
    public ICollection<Alert> Alerts { get; set; } = new List<Alert>();
    public ICollection<PatientMonitor> PatientMonitors { get; set; } = new List<PatientMonitor>();
}