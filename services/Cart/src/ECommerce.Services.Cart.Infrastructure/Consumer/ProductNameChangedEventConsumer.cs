using System.Text.Json;
using ECommerce.Shared.Messages;
using MassTransit;

namespace ECommerce.Services.Cart.Infrastructure.Consumer
;
public class ProductNameChangedEventConsumer : IConsumer<ProductNameChangedEvent>
{
    private readonly IRedisService _redisService;

    public ProductNameChangedEventConsumer(IRedisService redisService)
    {
        _redisService = redisService;
    }

    public async Task Consume(ConsumeContext<ProductNameChangedEvent> context)
    {
        var keys = _redisService.GetKeys();

        if (keys is not null)
        {
            foreach (var key in keys)
            {
                var redisValue = await _redisService.GetDb().StringGetAsync(key);
                var cart = JsonSerializer.Deserialize<CartDto>(redisValue);

                cart.CartItems.ForEach(i =>
                {
                    if (i.ProductId == context.Message.ProductId)
                    {
                        i.Name = context.Message.ProductName;
                    }
                });

                await _redisService.GetDb().StringSetAsync(key, JsonSerializer.Serialize(cart));
            }
        }

    }

}