namespace EmergencyPatientMonitoring.API.Entities;

public class Prediction
{
    public Guid Id { get; set; }

    public Guid SensorReadingId { get; set; }
    public SensorReading SensorReading { get; set; } = default!;

    public string RiskLevel { get; set; } = default!;
    public double Confidence { get; set; }
    public bool IsDangerous { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public Alert? Alert { get; set; }
}