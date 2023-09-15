using StackExchange.Redis;
namespace ECommerce.Services.Cart.Application.Common.Interfaces;
public interface IRedisService
{
    void Connect();
    IDatabase GetDb(int db=1);
    List<RedisKey> GetKeys();
}