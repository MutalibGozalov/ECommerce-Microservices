using ECommerce.Services.Cart.Application.Settings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;


namespace Microsoft.Extensions.DependencyInjection;
public static class ConfigureServices   
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<RedisSettings>(configuration.GetSection("RedisSettings"));
        services.AddSingleton<IRedisSettings>(serviceProvider => serviceProvider.GetRequiredService<IOptions<RedisSettings>>().Value); 


        return services;
    }
}