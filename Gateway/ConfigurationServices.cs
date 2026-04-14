using Gateway.Providers;
using Yarp.ReverseProxy.Configuration;

namespace Gateway;

public static class ConfigurationServices
{
    public static void AddReverseProxyServices(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddSingleton<DbProxyConfigProvider>();
        services.AddSingleton<IProxyConfigProvider>(sp =>
            sp.GetRequiredService<DbProxyConfigProvider>());
    }
}