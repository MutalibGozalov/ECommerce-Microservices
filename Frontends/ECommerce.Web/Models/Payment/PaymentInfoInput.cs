namespace ECommerce.Web.Models.Payment;
public class PaymentInfoInput
{
    public string CartName { get; set; } = null!;
    public string CartNumber { get; set; } = null!;
    public string Expriration { get; set; } = null!;
    public string CVV { get; set; } = null!;
    public decimal TotalPrice { get; set; }

}