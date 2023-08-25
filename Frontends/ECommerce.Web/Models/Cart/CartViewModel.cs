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
    public decimal GetTotalPrice
    {
        get => _cartItems.Sum(c => c.TotalPrice);
    }

    public decimal GetTotalDiscountPrice
    {
        get => _cartItems.Sum(c => c.TotalDiscountPrice);
    }

    public bool HasDiscount
    {
        get => string.IsNullOrEmpty(DiscountCode) is false && DiscountRate.HasValue;
    }


    public void ApplyDiscount(int rate, string code)
    {
        DiscountRate = rate;
        DiscountCode = code;
    }
    public void CancelDiscount()
    {
        DiscountCode = null;
        DiscountRate = null;
    }
}