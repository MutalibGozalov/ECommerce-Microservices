namespace ECommerce.Services.Cart.Application.Common.Dtos;
public class CartItemDto
{
    public string? ProductId { get; set; }
    public string? Name { get; set; }
    public decimal Price { get; set; }
    public int Quantity { get; set; }

}