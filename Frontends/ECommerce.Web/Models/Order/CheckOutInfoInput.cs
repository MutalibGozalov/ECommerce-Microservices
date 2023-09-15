namespace ECommerce.Web.Models.Order;
public class CheckOutInfoInput
{
    //order
    public string UserId { get; set; } = null!;
    public int ShippingId { get; set; }
    public int PaymentId { get; set; }
    public int ShippingAddressId { get; set; }

    //payment
    public string CartName { get; set; } = null!;
    public string CartNumber { get; set; } = null!;
    public string Expriration { get; set; } = null!;
    public string CVV { get; set; } = null!;
    public decimal TotalPrice { get; set; }
}