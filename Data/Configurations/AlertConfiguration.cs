using EmergencyPatientMonitoring.API.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EmergencyPatientMonitoring.API.Data.Configurations;

public class AlertConfiguration : IEntityTypeConfiguration<Alert>
{
    public void Configure(EntityTypeBuilder<Alert> b)
    {
        b.Property(x => x.Severity).HasMaxLength(30).IsRequired();
        b.Property(x => x.Status).HasMaxLength(30).IsRequired();

        b.HasOne(x => x.Prediction).WithOne(p => p.Alert)
            .HasForeignKey<Alert>(a => a.PredictionId).OnDelete(DeleteBehavior.Cascade);

        b.HasOne(x => x.Patient).WithMany(p => p.Alerts)
            .HasForeignKey(x => x.PatientId).OnDelete(DeleteBehavior.NoAction);

        b.HasOne(x => x.AcknowledgedByMonitor).WithMany(m => m.AcknowledgedAlerts)
            .HasForeignKey(x => x.AcknowledgedByMonitorId).OnDelete(DeleteBehavior.NoAction);

        b.HasOne(x => x.EmergencyService).WithMany(e => e.Alerts)
            .HasForeignKey(x => x.EmergencyServiceId).OnDelete(DeleteBehavior.SetNull);
    }
}