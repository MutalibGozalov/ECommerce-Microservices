namespace ECommerce.Web.Models.Cart;
public class CartViewModel
{
    public CartViewModel()
    {
        CartItems = new();
    }
    public string UserId { get; set; } = null!;
    public string? DiscountCode { get; set; }
    public int? DiscountRate { get; set; }
    
    private List<CartItemViewModel>? _cartItems;

    public List<CartItemViewModel>? CartItems
    {
        get
        {
            if (HasDiscount)
            {
                _cartItems.ForEach(item =>
                {
                    var discountPrice = item.Price * ((decimal)DiscountRate.Value / 100);
                    item.AppliedDiscount(Math.Round(item.Price - discountPrice, 2));
                });
            }
            return _cartItems;
        }
        set
        {
            _cartItems = value;
        }
    }
    public decimal TotalPrice
    {
        get => _cartItems.Sum(c => c.GetCurrentPrice * c.Quantity);
    }

    public bool HasDiscount
    {
        get => string.IsNullOrEmpty(DiscountCode) is false;
    }
}