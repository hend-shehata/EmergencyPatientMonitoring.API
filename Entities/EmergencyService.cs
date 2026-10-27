namespace EmergencyPatientMonitoring.API.Entities;

public class EmergencyService
{
    public Guid Id { get; set; }
    public string Name { get; set; } = default!;
    public string Phone { get; set; } = default!;
    public string ServiceType { get; set; } = default!;

    public ICollection<Alert> Alerts { get; set; } = new List<Alert>();
    public ICollection<Location> Locations { get; set; } = new List<Location>();
}