namespace DEPI_ECommerce_API.Entities;

public class OrderDetail
{
    public int OrderId { get; set; }
    public int ProductId { get; set; }
    public int Quantity { get; set; }
    public Order? Order { get; set; }
    public Product? Product { get; set; }
}
