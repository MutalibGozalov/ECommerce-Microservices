using System.Reflection.Metadata.Ecma335;

namespace ECommerce.Web.Models.Cart;
public class CartItemViewModel
{
    public string? ProductId { get; set; }
    public string? Name { get; set; }
    public decimal Price { get; set; }
    public byte Quantity { get; set; }

    public decimal? DiscountPrice { get; set; }

    private decimal? _TotalPrice;
    public decimal TotalPrice
    {
        get => Price*(decimal)Quantity;
    }

    private decimal? _TotalDiscountPrice;
    public decimal TotalDiscountPrice
    {
        get => DiscountPrice is not null ? DiscountPrice.Value*(decimal)Quantity : Price*(decimal)Quantity;
    }





    public void AppliedDiscount(decimal discountPrice)
    {
        DiscountPrice = discountPrice;
    }
}