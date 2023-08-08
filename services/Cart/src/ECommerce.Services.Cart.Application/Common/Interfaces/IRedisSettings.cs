namespace ECommerce.Services.Cart.Application.Common.Interfaces;
public interface IRedisSettings
{
    string? Host { get; set; }
    int Port { get; set; }
}
