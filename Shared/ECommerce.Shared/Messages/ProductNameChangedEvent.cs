namespace ECommerce.Shared.Messages
{
    public class ProductNameChangedEvent
    {
        public string ProductId { get; set; }
        public string ProductName { get; set; }
    }
}