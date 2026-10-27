namespace EmergencyPatientMonitoring.API.Entities;

public class SensorReading
{
    public Guid Id { get; set; }

    public Guid DeviceId { get; set; }
    public Device Device { get; set; } = default!;

    public Guid PatientId { get; set; }
    public Patient Patient { get; set; } = default!;

    public double HeartRate { get; set; }
    public double SpO2 { get; set; }
    public double Temperature { get; set; }
    public DateTime Timestamp { get; set; } = DateTime.UtcNow;

    public Prediction? Prediction { get; set; }
}