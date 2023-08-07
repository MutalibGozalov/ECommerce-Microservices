namespace ECommerce.Services.Cart.Application.Settings;
public class RedisSettings : IRedisSettings
{
    public string? Host { get; set; }
    public int Port { get; set; }
}