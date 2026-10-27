using EmergencyPatientMonitoring.API.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Monitor = EmergencyPatientMonitoring.API.Entities.Monitor;

namespace EmergencyPatientMonitoring.API.Data.Configurations;

public class PatientMonitorConfiguration : IEntityTypeConfiguration<PatientMonitor>
{
    public void Configure(EntityTypeBuilder<PatientMonitor> b)
    {
        b.HasKey(x => new { x.PatientId, x.MonitorId });
        b.Property(x => x.RelationshipToPatient).HasMaxLength(50).IsRequired();

        b.HasOne(x => x.Patient).WithMany(p => p.PatientMonitors)
            .HasForeignKey(x => x.PatientId).OnDelete(DeleteBehavior.Cascade);

        b.HasOne(x => x.Monitor).WithMany(m => m.PatientMonitors)
            .HasForeignKey(x => x.MonitorId).OnDelete(DeleteBehavior.Restrict);
    }
}