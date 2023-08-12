using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ECommerce.Services.Order.Infrastructure.Persistance.Configurations;

public class OrderDetailConfiguration : IEntityTypeConfiguration<OrderDetail>
{
    public void Configure(EntityTypeBuilder<OrderDetail> builder)
    {
        builder.Property(o => o.ProductPrice).HasColumnType("decimal(16,2)");
    }
}
