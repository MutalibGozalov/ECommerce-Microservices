using ECommerce.Shared.Messages;
using MassTransit;

namespace ECommerce.Services.Order.Application.Consumer;
public class ProductNameChangedEventConsumer : IConsumer<ProductNameChangedEvent>
{
    private readonly IAppDbContext _context;

    public ProductNameChangedEventConsumer(IAppDbContext context)
    {
        _context = context;
    }


    public async Task Consume(ConsumeContext<ProductNameChangedEvent> context)
    {
        var orderProducts = await _context.OrderDetails.Where(i => i.ProductId == context.Message.ProductId).ToListAsync();

        orderProducts.ForEach(p => p.ProductName = context.Message.ProductName);
        await _context.SaveChangesAsync();
    }

}