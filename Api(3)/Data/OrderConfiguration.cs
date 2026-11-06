using DEPI_ECommerce_API.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DEPI_ECommerce_API.Data.Configurations;

public class OrderConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.ToTable("Orders");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.OrderDate).IsRequired();

        builder.HasMany(x => x.OrderDetails)
            .WithOne(x => x.Order)
            .HasForeignKey(x => x.OrderId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasData(
            new Order { Id = 1, OrderDate = new DateTime(2026, 1, 10), CustomerId = 1 },
            new Order { Id = 2, OrderDate = new DateTime(2026, 2, 15), CustomerId = 2 }
        );
    }
}
