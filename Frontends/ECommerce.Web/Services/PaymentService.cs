using ECommerce.Web.Models.Payment;
using ECommerce.Web.Services.Interfaces;

namespace ECommerce.Web.Services;
public class PaymentService : IPaymentService
{
    private readonly HttpClient _httpClient;

    public PaymentService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<bool> ReceivePayment(PaymentInfoInput paymentInfoInput)
    {
        var response = await _httpClient.PostAsJsonAsync("Payment", paymentInfoInput);
        return response.IsSuccessStatusCode;
    }
}