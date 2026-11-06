using DEPI_ECommerce_API.Data;
using DEPI_ECommerce_API.DTOs;
using DEPI_ECommerce_API.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DEPI_ECommerce_API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CustomersController : ControllerBase
{
    private readonly ECommerceDbContext _context;
    public CustomersController(ECommerceDbContext context) => _context = context;

    [HttpGet]
    public async Task<ActionResult<IEnumerable<CustomerReadDto>>> GetAll()
    {
        var customers = await _context.Customers.AsNoTracking()
            .Select(c => new CustomerReadDto(c.Id, c.Name, c.Email)).ToListAsync();
        return Ok(customers);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<CustomerReadDto>> GetById(int id)
    {
        var customer = await _context.Customers.AsNoTracking().Where(c => c.Id == id)
            .Select(c => new CustomerReadDto(c.Id, c.Name, c.Email)).FirstOrDefaultAsync();
        return customer is null ? NotFound() : Ok(customer);
    }

    [HttpPost]
    public async Task<ActionResult<CustomerReadDto>> Create(CustomerCreateDto dto)
    {
        var customer = new Customer { Name = dto.Name, Email = dto.Email };
        _context.Customers.Add(customer);
        await _context.SaveChangesAsync();
        var result = new CustomerReadDto(customer.Id, customer.Name, customer.Email);
        return CreatedAtAction(nameof(GetById), new { id = customer.Id }, result);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, CustomerUpdateDto dto)
    {
        var customer = await _context.Customers.FindAsync(id);
        if (customer is null) return NotFound();
        customer.Name = dto.Name;
        customer.Email = dto.Email;
        await _context.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var customer = await _context.Customers.FindAsync(id);
        if (customer is null) return NotFound();
        _context.Customers.Remove(customer);
        try { await _context.SaveChangesAsync(); }
        catch (DbUpdateException) { return Conflict("Cannot delete a customer that has orders."); }
        return NoContent();
    }
}
