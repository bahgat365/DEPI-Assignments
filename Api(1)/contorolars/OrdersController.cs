using DEPI_ECommerce_API.Data;
using DEPI_ECommerce_API.DTOs;
using DEPI_ECommerce_API.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DEPI_ECommerce_API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OrdersController : ControllerBase
{
    private readonly ECommerceDbContext _context;
    public OrdersController(ECommerceDbContext context) => _context = context;

    [HttpGet]
    public async Task<ActionResult<IEnumerable<OrderReadDto>>> GetAll()
    {
        var orders = await _context.Orders.AsNoTracking()
            .Select(o => new OrderReadDto(o.Id, o.OrderDate, o.CustomerId, o.Customer!.Name))
            .ToListAsync();
        return Ok(orders);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<OrderReadDto>> GetById(int id)
    {
        var order = await _context.Orders.AsNoTracking().Where(o => o.Id == id)
            .Select(o => new OrderReadDto(o.Id, o.OrderDate, o.CustomerId, o.Customer!.Name))
            .FirstOrDefaultAsync();
        return order is null ? NotFound() : Ok(order);
    }

    [HttpPost]
    public async Task<ActionResult<OrderReadDto>> Create(OrderCreateDto dto)
    {
        if (!await _context.Customers.AnyAsync(c => c.Id == dto.CustomerId))
            return BadRequest("CustomerId does not exist.");

        var order = new Order { OrderDate = dto.OrderDate, CustomerId = dto.CustomerId };
        _context.Orders.Add(order);
        await _context.SaveChangesAsync();
        var result = new OrderReadDto(order.Id, order.OrderDate, order.CustomerId, null);
        return CreatedAtAction(nameof(GetById), new { id = order.Id }, result);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, OrderUpdateDto dto)
    {
        var order = await _context.Orders.FindAsync(id);
        if (order is null) return NotFound();
        if (!await _context.Customers.AnyAsync(c => c.Id == dto.CustomerId))
            return BadRequest("CustomerId does not exist.");
        order.OrderDate = dto.OrderDate;
        order.CustomerId = dto.CustomerId;
        await _context.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var order = await _context.Orders.Include(o => o.OrderDetails).FirstOrDefaultAsync(o => o.Id == id);
        if (order is null) return NotFound();
        _context.OrderDetails.RemoveRange(order.OrderDetails);
        _context.Orders.Remove(order);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}
