using System.Collections.Generic;

namespace ECommerce.Shared.Messages
{
    public class CreateOrderMessageCommand
    {
        public CreateOrderMessageCommand()
        {
            OrderItems = new List<OrderItem>();
        }

        public string UserId { get; set; } = null!;
        public int ShippingId { get; set; }
        public int PaymentId { get; set; }
        public int ShippingAddressId { get; set; }

        public List<OrderItem> OrderItems { get; set; }


    }

    public class OrderItem
    {
        public int OrderId { get; set; }
        public string ProductId { get; set; } = null!;
        public string ProductName { get; set; } = null!;
        public decimal ProductPrice { get; set; }
        public byte ProductQuantity { get; set; }
        public int TrackingId { get; set; }
    }

}