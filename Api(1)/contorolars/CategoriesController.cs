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
    public CategoriesController(ECommerceDbContext context) => _context = context;

    [HttpGet]
    public async Task<ActionResult<IEnumerable<CategoryReadDto>>> GetAll()
    {
        var categories = await _context.Categories.AsNoTracking()
            .Select(c => new CategoryReadDto(c.Id, c.Name)).ToListAsync();
        return Ok(categories);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<CategoryReadDto>> GetById(int id)
    {
        var category = await _context.Categories.AsNoTracking()
            .Where(c => c.Id == id)
            .Select(c => new CategoryReadDto(c.Id, c.Name)).FirstOrDefaultAsync();
        return category is null ? NotFound() : Ok(category);
    }

    [HttpPost]
    public async Task<ActionResult<CategoryReadDto>> Create(CategoryCreateDto dto)
    {
        var category = new Category { Name = dto.Name };
        _context.Categories.Add(category);
        await _context.SaveChangesAsync();
        var result = new CategoryReadDto(category.Id, category.Name);
        return CreatedAtAction(nameof(GetById), new { id = category.Id }, result);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, CategoryUpdateDto dto)
    {
        var category = await _context.Categories.FindAsync(id);
        if (category is null) return NotFound();
        category.Name = dto.Name;
        await _context.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var category = await _context.Categories.FindAsync(id);
        if (category is null) return NotFound();
        _context.Categories.Remove(category);
        try { await _context.SaveChangesAsync(); }
        catch (DbUpdateException) { return Conflict("Cannot delete a category that still has products."); }
        return NoContent();
    }
}
