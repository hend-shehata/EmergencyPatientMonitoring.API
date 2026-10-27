using EmergencyPatientMonitoring.API.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EmergencyPatientMonitoring.API.Data.Configurations;

public class PatientConfiguration : IEntityTypeConfiguration<Patient>
{
    public void Configure(EntityTypeBuilder<Patient> b)
    {
        b.HasKey(x => x.Id);
        b.HasIndex(x => x.PatientCode).IsUnique();
        b.Property(x => x.PatientCode).HasMaxLength(30).IsRequired();
        b.Property(x => x.BloodType).HasMaxLength(5);

        b.HasOne(x => x.HealthHistory).WithOne(h => h.Patient)
            .HasForeignKey<HealthHistory>(h => h.PatientId).OnDelete(DeleteBehavior.Cascade);

        b.HasOne(x => x.MobileApp).WithOne(m => m.Patient)
            .HasForeignKey<MobileApp>(m => m.PatientId).OnDelete(DeleteBehavior.Cascade);
    }
}