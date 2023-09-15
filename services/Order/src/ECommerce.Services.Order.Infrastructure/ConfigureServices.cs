using Microsoft.Extensions.Configuration;
using ECommerce.Shared.Services;
namespace Microsoft.Extensions.DependencyInjection;
public static class ConfigureServices
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        //Context
        services.AddHttpContextAccessor();
        services.AddScoped<ISharedIdentityService, SharedIdentityService>();

        //DbContext
        services.AddScoped<AuditableEntitySaveChangesInterceptor>();
        services.AddDbContext<AppDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"),
            builder => builder.MigrationsAssembly(typeof(AppDbContext).Assembly.FullName)));

        services.AddScoped<IAppDbContext>(provider => provider.GetRequiredService<AppDbContext>());

        services.AddScoped<AddDbContextInitializer>();


        // DateTime service
        services.AddTransient<IDateTime, DateTimeService>();

        return services;
    }
}
//RUN IN API DIR

// dotnet ef migrations add mig_1 -o ..\ECommerce.Services.Order.Infrastructure\Persistance -p ..\ECommerce.Services.Order.Infrastructure\ECommerce.Services.Order.Infrastructure.csproj