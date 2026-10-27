namespace EmergencyPatientMonitoring.API.Entities;

public class Admin
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public ApplicationUser User { get; set; } = default!;

    public ICollection<Device> ManagedDevices { get; set; } = new List<Device>();
}