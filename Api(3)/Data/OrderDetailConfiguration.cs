using DEPI_ECommerce_API.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DEPI_ECommerce_API.Data.Configurations;

public class OrderDetailConfiguration : IEntityTypeConfiguration<OrderDetail>
{
    public void Configure(EntityTypeBuilder<OrderDetail> builder)
    {
        builder.ToTable("OrderDetails");
        builder.HasKey(x => new { x.OrderId, x.ProductId });
        builder.Property(x => x.Quantity).IsRequired();

        builder.HasData(
            new OrderDetail { OrderId = 1, ProductId = 1, Quantity = 2 },
            new OrderDetail { OrderId = 1, ProductId = 2, Quantity = 1 },
            new OrderDetail { OrderId = 2, ProductId = 3, Quantity = 1 }
        );
    }
}
