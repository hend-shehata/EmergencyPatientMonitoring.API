using EmergencyPatientMonitoring.API.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Monitor = EmergencyPatientMonitoring.API.Entities.Monitor;

namespace EmergencyPatientMonitoring.API.Data.Configurations;

public class ApplicationUserConfiguration : IEntityTypeConfiguration<ApplicationUser>
{
    public void Configure(EntityTypeBuilder<ApplicationUser> b)
    {
        b.Property(x => x.Name).HasMaxLength(150).IsRequired();
        b.Property(x => x.AccountStatus).HasMaxLength(30).IsRequired();

        b.HasOne(x => x.Patient).WithOne(p => p.User)
            .HasForeignKey<Patient>(p => p.UserId).OnDelete(DeleteBehavior.Cascade);

        b.HasOne(x => x.Monitor).WithOne(m => m.User)
            .HasForeignKey<Monitor>(m => m.UserId).OnDelete(DeleteBehavior.Cascade);

        b.HasOne(x => x.Admin).WithOne(a => a.User)
            .HasForeignKey<Admin>(a => a.UserId).OnDelete(DeleteBehavior.Cascade);
    }
}