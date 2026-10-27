using EmergencyPatientMonitoring.API.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Monitor = EmergencyPatientMonitoring.API.Entities.Monitor;

namespace EmergencyPatientMonitoring.API.Data;

public class AppDbContext : IdentityDbContext<ApplicationUser, IdentityRole<Guid>, Guid>
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Patient> Patients => Set<Patient>();
    public DbSet<Monitor> Monitors => Set<Monitor>();
    public DbSet<Admin> Admins => Set<Admin>();
    public DbSet<PatientMonitor> PatientMonitors => Set<PatientMonitor>();
    public DbSet<HealthHistory> HealthHistories => Set<HealthHistory>();
    public DbSet<Device> Devices => Set<Device>();
    public DbSet<SensorReading> SensorReadings => Set<SensorReading>();
    public DbSet<Prediction> Predictions => Set<Prediction>();
    public DbSet<Alert> Alerts => Set<Alert>();
    public DbSet<EmergencyService> EmergencyServices => Set<EmergencyService>();
    public DbSet<MobileApp> MobileApps => Set<MobileApp>();
    public DbSet<Location> Locations => Set<Location>();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
    }
}