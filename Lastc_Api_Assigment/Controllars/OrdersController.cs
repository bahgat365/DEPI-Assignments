using AutoMapper;
using DEPI_ECommerce_API.Common;
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
    private readonly IMapper _mapper;
    private readonly ILogger<OrdersController> _logger;

    public OrdersController(ECommerceDbContext context, IMapper mapper, ILogger<OrdersController> logger)
    { _context = context; _mapper = mapper; _logger = logger; }

    [HttpGet]
    public async Task<ActionResult<Result<PagedResult<OrderReadDto>>>> GetAll(
        [FromQuery] PaginationParams page,
        [FromQuery] int? customerId,
        [FromQuery] DateTime? dateFrom,
        [FromQuery] DateTime? dateTo)
    {
        var query = _context.Orders.AsNoTracking().Include(o => o.Customer).AsQueryable();

        if (customerId.HasValue) query = query.Where(o => o.CustomerId == customerId.Value);
        if (dateFrom.HasValue) query = query.Where(o => o.OrderDate >= dateFrom.Value);
        if (dateTo.HasValue) query = query.Where(o => o.OrderDate <= dateTo.Value);

        var total = await query.CountAsync();
        var entities = await query.OrderByDescending(o => o.OrderDate)
            .Skip((page.PageNumber - 1) * page.PageSize).Take(page.PageSize).ToListAsync();
        var data = new PagedResult<OrderReadDto>
        {
            Items = _mapper.Map<List<OrderReadDto>>(entities),
            PageNumber = page.PageNumber, PageSize = page.PageSize, TotalCount = total
        };
        _logger.LogInformation("Retrieved orders page {PageNumber}; customer={CustomerId}", page.PageNumber, customerId);
        return Ok(Result<PagedResult<OrderReadDto>>.Success(data));
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<Result<OrderReadDto>>> GetById(int id)
    {
        var entity = await _context.Orders.AsNoTracking().Include(o => o.Customer)
            .FirstOrDefaultAsync(o => o.Id == id);
        return entity is null
            ? NotFound(Result<OrderReadDto>.Failure("Order not found."))
            : Ok(Result<OrderReadDto>.Success(_mapper.Map<OrderReadDto>(entity)));
    }

    [HttpPost]
    public async Task<ActionResult<Result<OrderReadDto>>> Create(OrderCreateDto dto)
    {
        if (!await _context.Customers.AnyAsync(c => c.Id == dto.CustomerId))
            return BadRequest(Result<OrderReadDto>.Failure("CustomerId does not exist."));
        var entity = _mapper.Map<Order>(dto);
        _context.Orders.Add(entity);
        await _context.SaveChangesAsync();
        _logger.LogInformation("Created order {OrderId}", entity.Id);
        var result = Result<OrderReadDto>.Success(_mapper.Map<OrderReadDto>(entity), "Order created.");
        return CreatedAtAction(nameof(GetById), new { id = entity.Id }, result);
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult<Result<OrderReadDto>>> Update(int id, OrderUpdateDto dto)
    {
        var entity = await _context.Orders.FindAsync(id);
        if (entity is null) return NotFound(Result<OrderReadDto>.Failure("Order not found."));
        if (!await _context.Customers.AnyAsync(c => c.Id == dto.CustomerId))
            return BadRequest(Result<OrderReadDto>.Failure("CustomerId does not exist."));
        _mapper.Map(dto, entity);
        await _context.SaveChangesAsync();
        _logger.LogInformation("Updated order {OrderId}", id);
        return Ok(Result<OrderReadDto>.Success(_mapper.Map<OrderReadDto>(entity), "Order updated."));
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult<Result<string>>> Delete(int id)
    {
        var entity = await _context.Orders.Include(o => o.OrderDetails).FirstOrDefaultAsync(o => o.Id == id);
        if (entity is null) return NotFound(Result<string>.Failure("Order not found."));
        _context.OrderDetails.RemoveRange(entity.OrderDetails);
        _context.Orders.Remove(entity);
        await _context.SaveChangesAsync();
        _logger.LogInformation("Deleted order {OrderId}", id);
        return Ok(Result<string>.Success("Deleted", "Order deleted."));
    }
}
