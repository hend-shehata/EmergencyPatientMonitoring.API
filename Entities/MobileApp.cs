namespace EmergencyPatientMonitoring.API.Entities;

public class MobileApp
{
    public Guid Id { get; set; }

    public Guid PatientId { get; set; }
    public Patient Patient { get; set; } = default!;

    public ICollection<Location> Locations { get; set; } = new List<Location>();
}