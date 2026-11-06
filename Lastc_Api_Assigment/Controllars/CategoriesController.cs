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
public class CategoriesController : ControllerBase
{
    private readonly ECommerceDbContext _context;
    private readonly IMapper _mapper;
    private readonly ILogger<CategoriesController> _logger;

    public CategoriesController(ECommerceDbContext context, IMapper mapper, ILogger<CategoriesController> logger)
    {
        _context = context; _mapper = mapper; _logger = logger;
    }

    [HttpGet]
    public async Task<ActionResult<Result<PagedResult<CategoryReadDto>>>> GetAll(
        [FromQuery] PaginationParams page, [FromQuery] string? search)
    {
        var query = _context.Categories.AsNoTracking();
        if (!string.IsNullOrWhiteSpace(search))
            query = query.Where(c => c.Name.Contains(search));

        var total = await query.CountAsync();
        var entities = await query.OrderBy(c => c.Id)
            .Skip((page.PageNumber - 1) * page.PageSize).Take(page.PageSize).ToListAsync();
        var data = new PagedResult<CategoryReadDto>
        {
            Items = _mapper.Map<List<CategoryReadDto>>(entities),
            PageNumber = page.PageNumber, PageSize = page.PageSize, TotalCount = total
        };
        _logger.LogInformation("Retrieved categories page {PageNumber}; search={Search}", page.PageNumber, search);
        return Ok(Result<PagedResult<CategoryReadDto>>.Success(data));
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<Result<CategoryReadDto>>> GetById(int id)
    {
        var entity = await _context.Categories.AsNoTracking().FirstOrDefaultAsync(c => c.Id == id);
        if (entity is null) return NotFound(Result<CategoryReadDto>.Failure("Category not found."));
        return Ok(Result<CategoryReadDto>.Success(_mapper.Map<CategoryReadDto>(entity)));
    }

    [HttpPost]
    public async Task<ActionResult<Result<CategoryReadDto>>> Create(CategoryCreateDto dto)
    {
        var entity = _mapper.Map<Category>(dto);
        _context.Categories.Add(entity);
        await _context.SaveChangesAsync();
        _logger.LogInformation("Created category {CategoryId}", entity.Id);
        var result = Result<CategoryReadDto>.Success(_mapper.Map<CategoryReadDto>(entity), "Category created.");
        return CreatedAtAction(nameof(GetById), new { id = entity.Id }, result);
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult<Result<CategoryReadDto>>> Update(int id, CategoryUpdateDto dto)
    {
        var entity = await _context.Categories.FindAsync(id);
        if (entity is null) return NotFound(Result<CategoryReadDto>.Failure("Category not found."));
        _mapper.Map(dto, entity);
        await _context.SaveChangesAsync();
        _logger.LogInformation("Updated category {CategoryId}", id);
        return Ok(Result<CategoryReadDto>.Success(_mapper.Map<CategoryReadDto>(entity), "Category updated."));
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult<Result<string>>> Delete(int id)
    {
        var entity = await _context.Categories.FindAsync(id);
        if (entity is null) return NotFound(Result<string>.Failure("Category not found."));
        _context.Categories.Remove(entity);
        try { await _context.SaveChangesAsync(); }
        catch (DbUpdateException ex)
        {
            _logger.LogWarning(ex, "Could not delete category {CategoryId}", id);
            return Conflict(Result<string>.Failure("Category is in use and cannot be deleted."));
        }
        _logger.LogInformation("Deleted category {CategoryId}", id);
        return Ok(Result<string>.Success("Deleted", "Category deleted."));
    }
}
