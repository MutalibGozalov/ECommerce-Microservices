namespace ECommerce.Services.Order.Application.Common.Interfaces;

public interface IAppDbContext
{
    DbSet<OrderModel> Orders { get; set; }
    DbSet<OrderDetail> OrderDetails { get; set; }
    DbSet<Shipping> Shippings { get; set; }
    DbSet<Tracking> Trackings { get; set; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}