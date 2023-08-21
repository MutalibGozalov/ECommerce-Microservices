using ECommerce.Shared.Messages;
using MassTransit;


namespace ECommerce.Services.Order.Application.Consumer;
public class CreateOrderMessageCommandConsumer : IConsumer<CreateOrderMessageCommand>
{
    private ISender _mediator;

    public CreateOrderMessageCommandConsumer(ISender mediator)
    {
        _mediator = mediator;
    }


    public async Task Consume(ConsumeContext<CreateOrderMessageCommand> context)
    {
        CreateOrderCommand createOrderCommand = new()
        {
            UserId = context.Message.UserId,
            ShippingId = context.Message.ShippingId,
            PaymentId = context.Message.PaymentId,
            ShippingAddressId = context.Message.ShippingAddressId
        };

        var createdOrder = await _mediator.Send(createOrderCommand);



        List<CreateOrderDetailCommand> orderDetailCommandList = new();

        foreach(var item in context.Message.OrderItems)
        {
            CreateTrackingCommand createTrackingCommand = new()
            {
                TrackingNumber = Guid.NewGuid().ToString()
            };

            var createTrackingResponse = await _mediator.Send(createTrackingCommand);
            int trackingId = createTrackingResponse.Data;

            orderDetailCommandList.Add(new() 
            {
                OrderId = createdOrder.Data.Id,
                ProductId = item.ProductId,
                ProductName = item.ProductName,
                ProductPrice= item.ProductPrice,
                ProductQuantity= item.ProductQuantity,
                TrackingId = trackingId
            });
        }


        foreach (var command in orderDetailCommandList)
        {
            await _mediator.Send(command);
        }

       
    }

}