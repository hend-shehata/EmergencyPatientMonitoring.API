using EmergencyPatientMonitoring.API.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EmergencyPatientMonitoring.API.Data.Configurations;

public class LocationConfiguration : IEntityTypeConfiguration<Location>
{
    public void Configure(EntityTypeBuilder<Location> b)
    {
        b.HasOne(x => x.MobileApp).WithMany(m => m.Locations)
            .HasForeignKey(x => x.MobileAppId).OnDelete(DeleteBehavior.Cascade);

        b.HasOne(x => x.EmergencyService).WithMany(e => e.Locations)
            .HasForeignKey(x => x.EmergencyServiceId).OnDelete(DeleteBehavior.SetNull);
    }
}