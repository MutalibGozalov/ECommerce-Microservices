using ECommerce.Shared.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;


namespace Microsoft.Extensions.DependencyInjection;
public static class ConfigureServices   
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        //Context
        services.AddHttpContextAccessor();
        services.AddScoped<ISharedIdentityService, SharedIdentityService>();
        
        //options
        services.Configure<RedisSettings>(configuration.GetSection("RedisSettings"));
        services.AddSingleton<IRedisSettings>(serviceProvider => serviceProvider.GetRequiredService<IOptions<RedisSettings>>().Value); 
        services.AddSingleton<IRedisService>(serviceProvider => {
            var redisSettings = serviceProvider.GetRequiredService<IOptions<RedisSettings>>().Value;

            var redis = new RedisService(redisSettings.Host, redisSettings.Port);

            redis.Connect();

            return redis;
        });



        // Cart service
        services.AddScoped<ICartService, CartService>();

        return services;
    }
}