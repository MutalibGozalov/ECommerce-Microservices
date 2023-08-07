namespace ECommerce.Services.Cart.Application.Settings;
public interface IRedisSettings
{
    string? Host { get; set; }
    int Port { get; set; }
}
