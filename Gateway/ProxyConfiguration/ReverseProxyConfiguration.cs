using Microsoft.Extensions.Primitives;
using Yarp.ReverseProxy.Configuration;

namespace Gateway.ProxyConfiguration;

public class ReverseProxyConfiguration : IProxyConfig
{
    public IReadOnlyList<RouteConfig> Routes { get; set; }
    public IReadOnlyList<ClusterConfig> Clusters { get; set; }
    public IChangeToken ChangeToken { get; set; }
}