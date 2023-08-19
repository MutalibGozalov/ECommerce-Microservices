namespace ECommerce.Services.Cart.Application.Common.Dtos;
public class CartDto
{
    public string? UserId { get; set; } = null!;
    public string? DiscountCode { get; set; }
    public int? DiscountRate { get; set; }
    public List<CartItemDto>? CartItems { get; set; }
    public decimal TotalPrice {
        get => CartItems.Sum(c => c.Price*c.Quantity);
    }
}