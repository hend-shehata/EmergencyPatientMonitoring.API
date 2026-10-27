using EmergencyPatientMonitoring.API.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EmergencyPatientMonitoring.API.Data.Configurations;

public class DeviceConfiguration : IEntityTypeConfiguration<Device>
{
    public void Configure(EntityTypeBuilder<Device> b)
    {
        b.Property(x => x.DeviceType).HasMaxLength(50).IsRequired();

        b.HasOne(x => x.Patient).WithMany(p => p.Devices)
            .HasForeignKey(x => x.PatientId).OnDelete(DeleteBehavior.Cascade);

        b.HasOne(x => x.ManagedByAdmin).WithMany(a => a.ManagedDevices)
     .HasForeignKey(x => x.ManagedByAdminId).OnDelete(DeleteBehavior.NoAction);

    }
}