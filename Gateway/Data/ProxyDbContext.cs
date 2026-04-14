using Gateway.Contracts;
using Gateway.Entities;
using Gateway.ProxyConfiguration;
using Microsoft.EntityFrameworkCore;
using Route = Gateway.Entities.Route;

namespace Gateway.Data;

public class ProxyDbContext(DbContextOptions<ProxyDbContext> options) : DbContext(options)
{
    public DbSet<Cluster> Clusters { get; set; }
    public DbSet<Destination> Destinations { get; set; }
    public DbSet<Route> Routes { get; set; }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        var utcNow = DateTime.UtcNow;

        foreach (var entry in ChangeTracker.Entries<IEntity>())
        {
            switch (entry.State)
            {
                case EntityState.Added:
                    entry.Entity.CreatedAt = utcNow;
                    entry.Entity.UpdatedAt = utcNow;
                    break;

                case EntityState.Modified:
                    entry.Entity.UpdatedAt = utcNow;
                    break;
            }
        }

        return await base.SaveChangesAsync(cancellationToken);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ClusterConfiguration).Assembly);
        base.OnModelCreating(modelBuilder);
    }

}