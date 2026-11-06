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
public class CustomersController : ControllerBase
{
    private readonly ECommerceDbContext _context;
    private readonly IMapper _mapper;
    private readonly ILogger<CustomersController> _logger;

    public CustomersController(ECommerceDbContext context, IMapper mapper, ILogger<CustomersController> logger)
    { _context = context; _mapper = mapper; _logger = logger; }

    [HttpGet]
    public async Task<ActionResult<Result<PagedResult<CustomerReadDto>>>> GetAll(
        [FromQuery] PaginationParams page, [FromQuery] string? search)
    {
        var query = _context.Customers.AsNoTracking();
        if (!string.IsNullOrWhiteSpace(search))
            query = query.Where(c => c.Name.Contains(search) || c.Email.Contains(search));

        var total = await query.CountAsync();
        var entities = await query.OrderBy(c => c.Id).Skip((page.PageNumber - 1) * page.PageSize)
            .Take(page.PageSize).ToListAsync();
        var data = new PagedResult<CustomerReadDto>
        {
            Items = _mapper.Map<List<CustomerReadDto>>(entities),
            PageNumber = page.PageNumber, PageSize = page.PageSize, TotalCount = total
        };
        _logger.LogInformation("Retrieved customers page {PageNumber}; search={Search}", page.PageNumber, search);
        return Ok(Result<PagedResult<CustomerReadDto>>.Success(data));
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<Result<CustomerReadDto>>> GetById(int id)
    {
        var entity = await _context.Customers.AsNoTracking().FirstOrDefaultAsync(c => c.Id == id);
        return entity is null
            ? NotFound(Result<CustomerReadDto>.Failure("Customer not found."))
            : Ok(Result<CustomerReadDto>.Success(_mapper.Map<CustomerReadDto>(entity)));
    }

    [HttpPost]
    public async Task<ActionResult<Result<CustomerReadDto>>> Create(CustomerCreateDto dto)
    {
        var entity = _mapper.Map<Customer>(dto);
        _context.Customers.Add(entity);
        try { await _context.SaveChangesAsync(); }
        catch (DbUpdateException ex)
        {
            _logger.LogWarning(ex, "Customer creation failed; email may already exist.");
            return Conflict(Result<CustomerReadDto>.Failure("Could not create customer. Email may already exist."));
        }
        _logger.LogInformation("Created customer {CustomerId}", entity.Id);
        var result = Result<CustomerReadDto>.Success(_mapper.Map<CustomerReadDto>(entity), "Customer created.");
        return CreatedAtAction(nameof(GetById), new { id = entity.Id }, result);
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult<Result<CustomerReadDto>>> Update(int id, CustomerUpdateDto dto)
    {
        var entity = await _context.Customers.FindAsync(id);
        if (entity is null) return NotFound(Result<CustomerReadDto>.Failure("Customer not found."));
        _mapper.Map(dto, entity);
        try { await _context.SaveChangesAsync(); }
        catch (DbUpdateException ex)
        {
            _logger.LogWarning(ex, "Customer update failed for {CustomerId}", id);
            return Conflict(Result<CustomerReadDto>.Failure("Could not update customer. Email may already exist."));
        }
        _logger.LogInformation("Updated customer {CustomerId}", id);
        return Ok(Result<CustomerReadDto>.Success(_mapper.Map<CustomerReadDto>(entity), "Customer updated."));
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult<Result<string>>> Delete(int id)
    {
        var entity = await _context.Customers.FindAsync(id);
        if (entity is null) return NotFound(Result<string>.Failure("Customer not found."));
        _context.Customers.Remove(entity);
        try { await _context.SaveChangesAsync(); }
        catch (DbUpdateException ex)
        {
            _logger.LogWarning(ex, "Could not delete customer {CustomerId}", id);
            return Conflict(Result<string>.Failure("Customer has orders and cannot be deleted."));
        }
        _logger.LogInformation("Deleted customer {CustomerId}", id);
        return Ok(Result<string>.Success("Deleted", "Customer deleted."));
    }
}
