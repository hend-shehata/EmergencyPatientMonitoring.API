namespace EmergencyPatientMonitoring.API.Entities;

public class Device
{
    public Guid Id { get; set; }

    public Guid PatientId { get; set; }
    public Patient Patient { get; set; } = default!;

    public Guid? ManagedByAdminId { get; set; }
    public Admin? ManagedByAdmin { get; set; }

    public string DeviceType { get; set; } = default!;
    public int BatteryLevel { get; set; }
    public DateTime LastSeen { get; set; }

    public ICollection<SensorReading> SensorReadings { get; set; } = new List<SensorReading>();
}