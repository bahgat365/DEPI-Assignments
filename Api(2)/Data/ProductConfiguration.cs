using DEPI_ECommerce_API.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DEPI_ECommerce_API.Data.Configurations;

public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.ToTable("Products");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Name).HasMaxLength(150).IsRequired();
        builder.Property(x => x.Price).HasColumnType("decimal(18,2)").IsRequired();

        builder.HasMany(x => x.OrderDetails)
            .WithOne(x => x.Product)
            .HasForeignKey(x => x.ProductId)
            .OnDelete(DeleteBehavior.Restrict);

        // Product is seeded because its CRUD endpoints are excluded by the previous assignment.
        builder.HasData(
            new Product { Id = 1, Name = "Wireless Mouse", Price = 450m, CategoryId = 1 },
            new Product { Id = 2, Name = "ASP.NET Core Guide", Price = 320m, CategoryId = 2 },
            new Product { Id = 3, Name = "Coffee Maker", Price = 1250m, CategoryId = 3 }
        );
    }
}
