namespace ECommerce.Web.Models.Order;
public class OrderCreatedViewModel
{
    public int Id { get; set; }
    public string? Error { get; set; }
    public bool IsSuccessfull { get; set; }
}