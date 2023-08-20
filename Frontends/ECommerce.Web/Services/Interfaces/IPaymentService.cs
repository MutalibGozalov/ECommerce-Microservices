using ECommerce.Web.Models.Payment;

namespace ECommerce.Web.Services.Interfaces;
public interface IPaymentService
{
    Task<bool> ReceivePayment(PaymentInfoInput paymentInfoInput);
}