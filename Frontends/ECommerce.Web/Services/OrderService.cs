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

    public async Task<OrderCreatedViewModel> CreateOrder(CheckOutInfoInput checkOutInfoInput)
    {
        var cart = await _cartService.Get();
        var paymentInfoInput = new PaymentInfoInput()
        {
            CartName = checkOutInfoInput.CartName,
            CartNumber = checkOutInfoInput.CartNumber,
            Expriration = checkOutInfoInput.Expriration,
            CVV = checkOutInfoInput.CVV,
            TotalPrice = cart.TotalPrice,
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

        cart.CartItems.ForEach(x => {
            
        });
    }

    public Task<List<OrderViewModel>> GetOrders()
    {
        throw new NotImplementedException();
    }

    public Task RequestOrder(CheckOutInfoInput checkOutInfoInput)
    {
        throw new NotImplementedException();
    }
}