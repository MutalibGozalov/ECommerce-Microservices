namespace ECommerce.Services.Order.Application.ShippingItems.Commands;
public class CreateShippingCommand : IRequest<Response<NoContent>>
{
    public string ShippingName { get; set; } = null!;
}