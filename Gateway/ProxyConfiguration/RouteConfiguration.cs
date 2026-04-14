using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Route = Gateway.Entities.Route;

namespace Gateway.ProxyConfiguration;

public class RouteConfiguration : IEntityTypeConfiguration<Route>
{
    public void Configure(EntityTypeBuilder<Entities.Route> builder)
    {
        
        builder.ToTable("Routes");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.RouteId)
            .IsRequired()
            .HasMaxLength(100);

        builder.HasIndex(x => x.RouteId)
            .IsUnique();

        builder.Property(x => x.Path)
            .IsRequired()
            .HasMaxLength(500);

        builder.HasOne(x => x.Cluster)
            .WithMany()
            .HasForeignKey(x => x.ClusterId)
            .OnDelete(DeleteBehavior.Restrict);
        
    }
}