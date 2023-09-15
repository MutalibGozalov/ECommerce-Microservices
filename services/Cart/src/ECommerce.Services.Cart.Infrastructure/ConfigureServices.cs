using ECommerce.Shared.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using ECommerce.Services.Cart.Infrastructure.Consumer;
using MassTransit;

namespace Microsoft.Extensions.DependencyInjection;
public static class ConfigureServices
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {

        //MassTransit
        services.AddMassTransit(m =>
        {
            m.AddConsumer<ProductNameChangedEventConsumer>();

            //port: 5672
            m.UsingRabbitMq((context, cfg) =>
            {
                cfg.Host(configuration["RabbitMQUrl"], "/", host =>
                {
                    host.Username("guest");
                    host.Password("guest");
                });

                cfg.ReceiveEndpoint("product-name-changed-event-cart-service", e =>
                {
                    e.ConfigureConsumer<ProductNameChangedEventConsumer>(context);
                });
            });
        });


        //Context
        services.AddHttpContextAccessor();
        services.AddScoped<ISharedIdentityService, SharedIdentityService>();

        //options
        services.Configure<RedisSettings>(configuration.GetSection("RedisSettings"));
        services.AddSingleton<IRedisSettings>(serviceProvider => serviceProvider.GetRequiredService<IOptions<RedisSettings>>().Value);
        services.AddSingleton<IRedisService>(serviceProvider =>
        {
            var redisSettings = serviceProvider.GetRequiredService<IOptions<RedisSettings>>().Value;

            var redis = new RedisService(redisSettings.Host ?? "localhost", redisSettings.Port);

            redis.Connect();

            return redis;
        });



        // Cart service
        services.AddScoped<ICartService, CartService>();

        return services;
    }
}