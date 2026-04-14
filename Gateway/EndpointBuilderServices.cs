using Gateway.Providers;

namespace Gateway;

public static class EndpointBuilderServices
{
    public static void ConfigureServices(this IEndpointRouteBuilder endpoints, IConfiguration configuration)
    {
        endpoints.MapPost("/proxy/reload", context =>
        {
            try
            {
                var proxyProvider = context.RequestServices
                    .GetRequiredService<DbProxyConfigProvider>();

                proxyProvider.ReloadConfig();

                context.Response.StatusCode = StatusCodes.Status204NoContent;
                return Task.CompletedTask;
            }
            catch (Exception exception)
            {
                context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                return Task.FromException(exception);
            }
        });
    }
}