using Gateway.Data;
using Gateway.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gateway.ProxyConfiguration;

public class DestinationConfiguration : IEntityTypeConfiguration<Destination>
{
    public void Configure(EntityTypeBuilder<Destination> builder)
    {
        
        builder.ToTable("Destinations");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Address)
            .IsRequired()
            .HasMaxLength(500);

        builder.HasOne(x => x.Cluster)
            .WithMany(x => x.Destinations)
            .HasForeignKey(x => x.ClusterId);
        
    }
}