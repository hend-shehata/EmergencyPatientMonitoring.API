using EmergencyPatientMonitoring.API.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EmergencyPatientMonitoring.API.Data.Configurations;

public class SensorReadingConfiguration : IEntityTypeConfiguration<SensorReading>
{
    public void Configure(EntityTypeBuilder<SensorReading> b)
    {
        b.HasIndex(x => new { x.PatientId, x.Timestamp });

        b.HasOne(x => x.Device).WithMany(d => d.SensorReadings)
            .HasForeignKey(x => x.DeviceId).OnDelete(DeleteBehavior.Cascade);

        // NoAction عشان SQL Server مبيسمحش بـ multiple cascade paths
        b.HasOne(x => x.Patient).WithMany(p => p.SensorReadings)
            .HasForeignKey(x => x.PatientId).OnDelete(DeleteBehavior.NoAction);

        b.HasOne(x => x.Prediction).WithOne(p => p.SensorReading)
            .HasForeignKey<Prediction>(p => p.SensorReadingId).OnDelete(DeleteBehavior.Cascade);
    }
}