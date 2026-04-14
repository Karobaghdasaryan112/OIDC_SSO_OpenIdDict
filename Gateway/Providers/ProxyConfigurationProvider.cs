using Gateway.Data;
using Gateway.ProxyConfiguration;
using Microsoft.Extensions.Primitives;
using Yarp.ReverseProxy.Configuration;

namespace Gateway.Providers;

public class DbProxyConfigProvider : IProxyConfigProvider
{
    private readonly IServiceProvider _serviceProvider;
    private ReverseProxyConfiguration _config;
    private readonly CancellationTokenSource _cts;

    public DbProxyConfigProvider(IServiceProvider serviceProvider, CancellationTokenSource cts)
    {
        _serviceProvider = serviceProvider;
        _cts = cts;
        _config = LoadFromDb();
    }

    public IProxyConfig GetConfig() => _config;

    private ReverseProxyConfiguration LoadFromDb()
    {
        using var scope = _serviceProvider.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<ProxyDbContext>();

        var routes = db.Routes
            .Select(r => new RouteConfig
            {
                RouteId = r.RouteId,
                ClusterId = r.Cluster.ClusterId,
                Match = new RouteMatch
                {
                    Path = r.Path,
                    Methods = r.Methods
                }
            })
            .ToList();

        var clusters = db.Clusters
            .Select(c => new ClusterConfig
            {
                ClusterId = c.ClusterId,
                Destinations = c.Destinations.ToDictionary(
                    d => d.Id.ToString(),
                    d => new DestinationConfig
                    {
                        Address = d.Address
                    })
            })
            .ToList();

        return new ReverseProxyConfiguration()
        {
            Routes = routes,
            Clusters = clusters,
            ChangeToken = new CancellationChangeToken(CancellationToken.None)
        };
    }

    public void ReloadConfig()
    {
        var oldCts = _cts;

        var cts = new CancellationTokenSource();

        _config = LoadFromDb();

        oldCts.Cancel();
    }
}