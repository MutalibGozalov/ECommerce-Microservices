using ECommerce.Shared.Messages;
using ECommerce.Shared.Services;
using MassTransit;

namespace ECommerce.Services.Cart.Infrastructure.Consumer
;
public class ProductNameChangedEventConsumer : IConsumer<ProductNameChangedEvent>
{
    private readonly ICartService _cartService;
    private readonly ISharedIdentityService _sharedIdentityService;

    public ProductNameChangedEventConsumer(ICartService cartService, ISharedIdentityService sharedIdentityService)
    {
        _cartService = cartService;
        _sharedIdentityService = sharedIdentityService;

    }

    public async Task Consume(ConsumeContext<ProductNameChangedEvent> context)
    {
        var response = await _cartService.GetCart(_sharedIdentityService.GetUserId);
        if (response.IsSuccessful)
        {
            var cartUpdate = response.Data;
            cartUpdate.CartItems.FirstOrDefault(i => i.ProductId == context.Message.ProductId).Name = context.Message.ProductName;
            _cartService.SaveOrUpdate(cartUpdate);
        }
       
    }

}