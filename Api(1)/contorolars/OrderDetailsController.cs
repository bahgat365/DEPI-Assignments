using DEPI_ECommerce_API.Data;
using DEPI_ECommerce_API.DTOs;
using DEPI_ECommerce_API.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DEPI_ECommerce_API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OrderDetailsController : ControllerBase
{
    private readonly ECommerceDbContext _context;
    public OrderDetailsController(ECommerceDbContext context) => _context = context;

    [HttpGet]
    public async Task<ActionResult<IEnumerable<OrderDetailReadDto>>> GetAll()
    {
        var details = await _context.OrderDetails.AsNoTracking()
            .Select(d => new OrderDetailReadDto(d.OrderId, d.ProductId, d.Quantity, d.Product!.Name))
            .ToListAsync();
        return Ok(details);
    }

    [HttpGet("{orderId:int}/{productId:int}")]
    public async Task<ActionResult<OrderDetailReadDto>> GetById(int orderId, int productId)
    {
        var detail = await _context.OrderDetails.AsNoTracking()
            .Where(d => d.OrderId == orderId && d.ProductId == productId)
            .Select(d => new OrderDetailReadDto(d.OrderId, d.ProductId, d.Quantity, d.Product!.Name))
            .FirstOrDefaultAsync();
        return detail is null ? NotFound() : Ok(detail);
    }

    [HttpPost]
    public async Task<ActionResult<OrderDetailReadDto>> Create(OrderDetailCreateDto dto)
    {
        if (!await _context.Orders.AnyAsync(o => o.Id == dto.OrderId))
            return BadRequest("OrderId does not exist.");
        if (!await _context.Products.AnyAsync(p => p.Id == dto.ProductId))
            return BadRequest("ProductId does not exist.");
        if (dto.Quantity <= 0) return BadRequest("Quantity must be greater than zero.");
        if (await _context.OrderDetails.AnyAsync(d => d.OrderId == dto.OrderId && d.ProductId == dto.ProductId))
            return Conflict("This product is already included in this order.");

        var detail = new OrderDetail { OrderId = dto.OrderId, ProductId = dto.ProductId, Quantity = dto.Quantity };
        _context.OrderDetails.Add(detail);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetById),
            new { orderId = detail.OrderId, productId = detail.ProductId },
            new OrderDetailReadDto(detail.OrderId, detail.ProductId, detail.Quantity, null));
    }

    [HttpPut("{orderId:int}/{productId:int}")]
    public async Task<IActionResult> Update(int orderId, int productId, OrderDetailUpdateDto dto)
    {
        var detail = await _context.OrderDetails.FindAsync(orderId, productId);
        if (detail is null) return NotFound();
        if (dto.Quantity <= 0) return BadRequest("Quantity must be greater than zero.");
        detail.Quantity = dto.Quantity;
        await _context.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{orderId:int}/{productId:int}")]
    public async Task<IActionResult> Delete(int orderId, int productId)
    {
        var detail = await _context.OrderDetails.FindAsync(orderId, productId);
        if (detail is null) return NotFound();
        _context.OrderDetails.Remove(detail);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}
