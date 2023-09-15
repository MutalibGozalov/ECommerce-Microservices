using ECommerce.Services.Payment.Models;
using ECommerce.Shared.ControllerBases;
using ResponseNoContent = ECommerce.Shared.Dtos.Response<ECommerce.Shared.Dtos.NoContent>;
using ECommerce.Shared.Messages;
using MassTransit;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Services.Payment.Controllers;
[Route("api/[controller]")]
[ApiController]
public class PaymentController : CustomBaseController
{
    private readonly ISendEndpointProvider _sendEndpointProvider;

    public PaymentController(ISendEndpointProvider sendEndpointProvider)
    {
        _sendEndpointProvider = sendEndpointProvider;
    }

    [HttpPost]
    public async Task<IActionResult> ReceivePayment(PaymentDto payment)
    {
        var sendEndpoint = await _sendEndpointProvider.GetSendEndpoint(new Uri("queue:create-order-service"));

        var createOrderMessageCommand = new CreateOrderMessageCommand
        {
            UserId = payment.Order.UserId,
            ShippingId = payment.Order.ShippingId,
            PaymentId = payment.Order.PaymentId,
            ShippingAddressId = payment.Order.ShippingAddressId
        };

        payment?.Order?.OrderItems?.ForEach(i =>
        {
            createOrderMessageCommand.OrderItems.Add(new OrderItem()
            {
                OrderId = i.OrderId,
                ProductId = i.ProductId,
                ProductName = i.ProductName,
                ProductPrice= i.ProductPrice,
                ProductQuantity= i.ProductQuantity,
                TrackingId = i.TrackingId
            });
        });

        await sendEndpoint.Send(createOrderMessageCommand);
 
        return CreateActionResultInstance(ResponseNoContent.Success(200));
    }
}