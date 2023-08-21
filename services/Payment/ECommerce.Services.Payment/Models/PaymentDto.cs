namespace ECommerce.Services.Payment.Models;
public class PaymentDto
{
    public string? CartName { get; set; } = null!;
    public string? CartNumber { get; set; } = null!;
    public string? Expriration { get; set; } = null!;
    public string? CVV { get; set; } = null!;
    public decimal? TotalPrice { get; set; }
    public OrderDto? Order { get; set; }
}