namespace ECommerce.Services.Cart.Infrastructure.Persistance.Concretes;
public class RedisSettings : IRedisSettings
{
    public string? Host { get; set; }
    public int Port { get; set; }
}