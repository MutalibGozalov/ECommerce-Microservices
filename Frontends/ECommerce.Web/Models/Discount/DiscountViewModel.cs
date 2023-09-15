namespace ECommerce.Web.Models.Discount;
public class DiscountViewModel
{
    public string? UserId { get; set; }
    public string Code { get; set; } = null!;
    public int Rate { get; set; }
}