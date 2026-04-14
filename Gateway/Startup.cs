using System.Net;
using Gateway.Data;
using Gateway.Providers;
using Microsoft.EntityFrameworkCore;

namespace Gateway;

public class Startup(IConfiguration configuration)
{
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddControllers();

        services.AddAuthentication();
        services.AddAuthorization();

        services.AddCors(options =>
        {
            options.AddPolicy("Default", policy =>
            {
                policy.AllowAnyOrigin()
                    .AllowAnyHeader()
                    .AllowAnyMethod();
            });
        });
        services.AddDbContext<ProxyDbContext>(options =>
            options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

        services.AddReverseProxyServices(Configuration);

        services.AddReverseProxy();

        services.AddSwaggerGen();

        services.AddEndpointsApiExplorer();

        services.AddRouting(options => options.LowercaseUrls = true);
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseRouting();

        app.UseCors("Default");

        app.UseAuthentication();
        app.UseAuthorization();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();

            endpoints.MapReverseProxy();

            endpoints.ConfigureServices(Configuration);
        });
    }

    private IConfiguration Configuration { get; } = configuration;
}