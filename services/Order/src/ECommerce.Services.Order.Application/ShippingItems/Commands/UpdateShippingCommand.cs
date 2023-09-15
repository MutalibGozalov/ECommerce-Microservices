namespace ECommerce.Services.Order.Application.ShippingItems.Commands
{
    public class UpdateShippingCommand : IRequest<Response<NoContent>>
    {
        public int Id { get; set; }
        public string ShippingName { get; set; } = null!;
    }
}