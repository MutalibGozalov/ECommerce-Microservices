namespace ECommerce.Services.Order.Application.ShippingItems.Commands;
public class DeleteShippingCommand : IRequest<Response<NoContent>>
{
    public int Id { get; set; }
}