namespace EmergencyPatientMonitoring.API.Entities;

public class Location
{
    public Guid Id { get; set; }

    public Guid MobileAppId { get; set; }
    public MobileApp MobileApp { get; set; } = default!;

    public Guid? EmergencyServiceId { get; set; }
    public EmergencyService? EmergencyService { get; set; }

    public double Latitude { get; set; }
    public double Longitude { get; set; }
    public DateTime Timestamp { get; set; } = DateTime.UtcNow;
}