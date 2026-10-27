using Microsoft.AspNetCore.Identity;

namespace EmergencyPatientMonitoring.API.Entities;

public class ApplicationUser : IdentityUser<Guid>
{
    public string Name { get; set; } = default!;
    public string AccountStatus { get; set; } = "Active";

    public Patient? Patient { get; set; }
    public Monitor? Monitor { get; set; }
    public Admin? Admin { get; set; }
}