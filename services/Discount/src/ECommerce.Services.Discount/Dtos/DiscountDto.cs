namespace ECommerce.Services.Discount.Dtos;
public class DiscountDto
{
    public string UserId { get; set; }
    public string Code { get; set; }
    public int Rate { get; set; }

    public DateTime CreatedTime { get; set; }
}