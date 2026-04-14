using Gateway.Data;
using Gateway.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gateway.ProxyConfiguration;

public class ClusterConfiguration : IEntityTypeConfiguration<Cluster>
{
    public void Configure(EntityTypeBuilder<Cluster> builder)
    {
        
        builder.ToTable("Clusters");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.ClusterId)
            .IsRequired()
            .HasMaxLength(100);

        builder.HasIndex(x => x.ClusterId)
            .IsUnique();

        builder.HasMany(x => x.Destinations)
            .WithOne(x => x.Cluster)
            .HasForeignKey(x => x.ClusterId)
            .OnDelete(DeleteBehavior.Cascade);
        
    }
}