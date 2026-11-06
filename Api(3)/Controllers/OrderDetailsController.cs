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
public class OrderDetailsController : ControllerBase
{
    private readonly ECommerceDbContext _context;
    private readonly IMapper _mapper;
    private readonly ILogger<OrderDetailsController> _logger;

    public OrderDetailsController(ECommerceDbContext context, IMapper mapper, ILogger<OrderDetailsController> logger)
    { _context = context; _mapper = mapper; _logger = logger; }

    [HttpGet]
    public async Task<ActionResult<Result<PagedResult<OrderDetailReadDto>>>> GetAll(
        [FromQuery] PaginationParams page,
        [FromQuery] int? orderId,
        [FromQuery] int? productId,
        [FromQuery] string? search)
    {
        var query = _context.OrderDetails.AsNoTracking().Include(d => d.Product).AsQueryable();
        if (orderId.HasValue) query = query.Where(d => d.OrderId == orderId.Value);
        if (productId.HasValue) query = query.Where(d => d.ProductId == productId.Value);
        if (!string.IsNullOrWhiteSpace(search))
            query = query.Where(d => d.Product != null && d.Product.Name.Contains(search));

        var total = await query.CountAsync();
        var entities = await query.OrderBy(d => d.OrderId).ThenBy(d => d.ProductId)
            .Skip((page.PageNumber - 1) * page.PageSize).Take(page.PageSize).ToListAsync();
        var data = new PagedResult<OrderDetailReadDto>
        {
            Items = _mapper.Map<List<OrderDetailReadDto>>(entities),
            PageNumber = page.PageNumber, PageSize = page.PageSize, TotalCount = total
        };
        _logger.LogInformation("Retrieved order details page {PageNumber}", page.PageNumber);
        return Ok(Result<PagedResult<OrderDetailReadDto>>.Success(data));
    }

    [HttpGet("{orderId:int}/{productId:int}")]
    public async Task<ActionResult<Result<OrderDetailReadDto>>> GetById(int orderId, int productId)
    {
        var entity = await _context.OrderDetails.AsNoTracking().Include(d => d.Product)
            .FirstOrDefaultAsync(d => d.OrderId == orderId && d.ProductId == productId);
        return entity is null
            ? NotFound(Result<OrderDetailReadDto>.Failure("Order detail not found."))
            : Ok(Result<OrderDetailReadDto>.Success(_mapper.Map<OrderDetailReadDto>(entity)));
    }

    [HttpPost]
    public async Task<ActionResult<Result<OrderDetailReadDto>>> Create(OrderDetailCreateDto dto)
    {
        if (dto.Quantity <= 0) return BadRequest(Result<OrderDetailReadDto>.Failure("Quantity must be greater than zero."));
        if (!await _context.Orders.AnyAsync(o => o.Id == dto.OrderId))
            return BadRequest(Result<OrderDetailReadDto>.Failure("OrderId does not exist."));
        if (!await _context.Products.AnyAsync(p => p.Id == dto.ProductId))
            return BadRequest(Result<OrderDetailReadDto>.Failure("ProductId does not exist."));
        if (await _context.OrderDetails.AnyAsync(d => d.OrderId == dto.OrderId && d.ProductId == dto.ProductId))
            return Conflict(Result<OrderDetailReadDto>.Failure("This product is already part of this order."));

        var entity = _mapper.Map<OrderDetail>(dto);
        _context.OrderDetails.Add(entity);
        await _context.SaveChangesAsync();
        _logger.LogInformation("Created order detail for order {OrderId} and product {ProductId}", dto.OrderId, dto.ProductId);
        var result = Result<OrderDetailReadDto>.Success(_mapper.Map<OrderDetailReadDto>(entity), "Order detail created.");
        return CreatedAtAction(nameof(GetById), new { orderId = entity.OrderId, productId = entity.ProductId }, result);
    }

    [HttpPut("{orderId:int}/{productId:int}")]
    public async Task<ActionResult<Result<OrderDetailReadDto>>> Update(int orderId, int productId, OrderDetailUpdateDto dto)
    {
        if (dto.Quantity <= 0) return BadRequest(Result<OrderDetailReadDto>.Failure("Quantity must be greater than zero."));
        var entity = await _context.OrderDetails.FindAsync(orderId, productId);
        if (entity is null) return NotFound(Result<OrderDetailReadDto>.Failure("Order detail not found."));
        _mapper.Map(dto, entity);
        await _context.SaveChangesAsync();
        _logger.LogInformation("Updated order detail for order {OrderId} and product {ProductId}", orderId, productId);
        return Ok(Result<OrderDetailReadDto>.Success(_mapper.Map<OrderDetailReadDto>(entity), "Order detail updated."));
    }

    [HttpDelete("{orderId:int}/{productId:int}")]
    public async Task<ActionResult<Result<string>>> Delete(int orderId, int productId)
    {
        var entity = await _context.OrderDetails.FindAsync(orderId, productId);
        if (entity is null) return NotFound(Result<string>.Failure("Order detail not found."));
        _context.OrderDetails.Remove(entity);
        await _context.SaveChangesAsync();
        _logger.LogInformation("Deleted order detail for order {OrderId} and product {ProductId}", orderId, productId);
        return Ok(Result<string>.Success("Deleted", "Order detail deleted."));
    }
}
