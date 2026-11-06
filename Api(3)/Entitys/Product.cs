namespace DEPI_ECommerce_API.Entities;

// Product is included as a relationship entity, but has NO CRUD endpoints
// because the assignment says to exclude the first entity.
public class Product
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public int CategoryId { get; set; }
    public Category? Category { get; set; }
    public ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();
}
