using ECommerce.Web.Models.Order;

namespace ECommerce.Web.Services.Interfaces;
public interface IOrderService
{
    //Synchron reqeuest
    Task<OrderCreatedViewModel> CreateOrder(CheckOutInfoInput checkOutInfoInput);

    //Async request with RabbitMQ
    Task RequestOrder(CheckOutInfoInput checkOutInfoInput);

    Task<List<OrderViewModel>> GetOrders();
}