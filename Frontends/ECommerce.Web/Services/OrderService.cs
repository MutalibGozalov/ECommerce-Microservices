using ECommerce.Shared.Dtos;
using ECommerce.Shared.Services;
using ECommerce.Web.Models.Order;
using ECommerce.Web.Models.Payment;
using ECommerce.Web.Services.Interfaces;

namespace ECommerce.Web.Services;
public class OrderService : IOrderService
{
    private readonly HttpClient _httpClient;
    private readonly IPaymentService _paymentService;
    private readonly ICartService _cartService;
    private readonly ISharedIdentityService _sharedIdentityService;

    public OrderService(HttpClient httpClient, IPaymentService paymentService, ICartService cartService, ISharedIdentityService sharedIdentityService)
    {
        _httpClient = httpClient;
        _paymentService = paymentService;
        _cartService = cartService;
        _sharedIdentityService = sharedIdentityService;
    }

    public async Task<List<OrderViewModel>> GetOrders()
    {
        var responseOrder = await _httpClient.GetFromJsonAsync<Response<List<OrderViewModel>>>("order");
        var responseItem = await _httpClient.GetFromJsonAsync<Response<List<OrderItemViewModel>>>("OrderDetail");
        responseOrder?.Data.ForEach(o =>
        {
            o.OrderItem = responseItem.Data.Where(i => i.OrderId == o.Id).ToList();
        });
        return responseOrder.Data;
    }


    public async Task<OrderCreatedViewModel> CreateOrder(CheckOutInfoInput checkOutInfoInput)
    {
        var cart = await _cartService.Get();
        var paymentInfoInput = new PaymentInfoInput()
        {
            CartName = checkOutInfoInput.CartName,
            CartNumber = checkOutInfoInput.CartNumber,
            Expriration = checkOutInfoInput.Expriration,
            CVV = checkOutInfoInput.CVV,
            TotalPrice = cart.GetTotalDiscountPrice,
        };

        var responsePayment = await _paymentService.ReceivePayment(paymentInfoInput);

        if (responsePayment is false)
            return new OrderCreatedViewModel() { Error = "Payment could not be cpmleted", IsSuccessfull = false };

        OrderCreateInput orderCreateInput = new()
        {
            UserId = _sharedIdentityService.GetUserId,
            ShippingId = checkOutInfoInput.ShippingId,
            PaymentId = checkOutInfoInput.PaymentId,
            ShippingAddressId = checkOutInfoInput.ShippingAddressId,
        };

        cart.CartItems.ForEach(i =>
        {
            orderCreateInput.OrderItems.Add(
                    new OrderItemCreateInput()
                    {
                        ProductId = i.ProductId,
                        ProductPrice = i.TotalDiscountPrice,
                        ProductName = i.Name,
                        ProductQuantity = i.Quantity,
                        TrackingId = Random.Shared.Next(0, 500)
                    }
                );
        });

        var response = await _httpClient.PostAsJsonAsync("order", orderCreateInput);

        if (response.IsSuccessStatusCode is false)
        {
            return new OrderCreatedViewModel() { Error = "Order could not be completed DARLIN!", IsSuccessfull = false };
        }

        var orderCreatedViewModel = await response.Content.ReadFromJsonAsync<Response<OrderCreatedViewModel>>();
        orderCreatedViewModel.Data.IsSuccessfull = true;

        await _cartService.Delete();

        return orderCreatedViewModel.Data;
    }


    public async Task<OrderRequestedViewModel> RequestOrder(CheckOutInfoInput checkOutInfoInput)
    {


        var cart = await _cartService.Get();

        OrderCreateInput orderCreateInput = new()
        {
            UserId = _sharedIdentityService.GetUserId,
            ShippingId = checkOutInfoInput.ShippingId,
            PaymentId = checkOutInfoInput.PaymentId,
            ShippingAddressId = checkOutInfoInput.ShippingAddressId,
        };

        cart.CartItems.ForEach(i =>
        {
            orderCreateInput.OrderItems.Add(
                    new OrderItemCreateInput()
                    {
                        ProductId = i.ProductId,
                        ProductPrice = i.TotalDiscountPrice,
                        ProductName = i.Name,
                        ProductQuantity = i.Quantity,
                        TrackingId = Random.Shared.Next(0, 500)
                    }
                );
        });

        var paymentInfoInput = new PaymentInfoInput()
        {
            CartName = checkOutInfoInput.CartName,
            CartNumber = checkOutInfoInput.CartNumber,
            Expriration = checkOutInfoInput.Expriration,
            CVV = checkOutInfoInput.CVV,
            TotalPrice = cart.GetTotalDiscountPrice,
            Order = orderCreateInput
        };

        var responsePayment = await _paymentService.ReceivePayment(paymentInfoInput);

        if (responsePayment is false)
            return new OrderRequestedViewModel() { Error = "Payment could not be completed DARLIN", IsSuccessfull = false };

        await _cartService.Delete();

        return new OrderRequestedViewModel() { IsSuccessfull = true };
    }
}