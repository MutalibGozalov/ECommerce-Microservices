using System.Reflection.Metadata.Ecma335;

namespace ECommerce.Web.Models.Cart;
public class CartItemViewModel
{
    public string? ProductId { get; set; }
    public string? Name { get; set; }
    public decimal Price { get; set; }
    public int Quantity { get; set; }

    private decimal? DiscountPrice;
    public decimal GetCurrentPrice
    {
        get => DiscountPrice is not null ? DiscountPrice.Value : Price;
    }
    public void AppliedDiscount(decimal discountPrice)
    {
        DiscountPrice = discountPrice;
    }
}