using Dapper.Contrib.Extensions;

namespace ECommerce.Services.Discount.Models;
[Table("discount")]
public class Discount
{
    public int Id { get; set; }
    public string? UserId { get; set; }
    public string Code { get; set; } = null!;
    public int Rate { get; set; }
    public DateTime CreatedTime { get; set; }
}