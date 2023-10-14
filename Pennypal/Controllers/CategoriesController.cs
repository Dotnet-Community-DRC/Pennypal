using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pennypal.Data;
using Pennypal.DTOs;
using Pennypal.Entities;

namespace Pennypal.Controllers;

public class CategoriesController : BaseApiController
{
    private readonly AppDbContext _context;
    private readonly ILogger<CategoriesController> _logger;
    private readonly IMapper _mapper;

    public CategoriesController(AppDbContext context, ILogger<CategoriesController> logger, IMapper mapper)
    {
        _context = context;
        _logger = logger;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult<List<Category>>> GetCategories()
    {
        return await _context.Categories.ToListAsync();
    }

    [HttpGet("{id}", Name = "GetCategory")]
    public async Task<ActionResult<Category>> GetCategory(Guid id)
    {
        var category = await _context.Categories.FindAsync(id);

        if (category is null) return NotFound();

        return category;
    }

    [HttpPost]
    public async Task<ActionResult<Category>> CreateCategory([FromForm] CreateCategoryDto categoryDto)
    {
        var category = _mapper.Map<Category>(categoryDto);

        _context.Categories.Add(category);

        var result = await _context.SaveChangesAsync() > 0;

        if (result) return CreatedAtRoute("GetCategory", new { Id = category.Guid }, category);

        return BadRequest(new ProblemDetails { Title = "Problem saving Category" });
    }

    [HttpPut]
    public async Task<ActionResult<Category>> UpdateCategory([FromForm] UpdateCategoryDto categoryDto)
    {
        var category = await _context.Categories.FindAsync(categoryDto.Id);

        if (category is null) return NotFound();

        _mapper.Map(category, categoryDto);

        var result = await _context.SaveChangesAsync() > 0;

        if (result) return Ok(category);

        return BadRequest(new ProblemDetails { Title = "Problem Updating the Category" });
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteCategory(Guid id)
    {
        var category = await _context.Categories.FindAsync(id);

        if (category is null) return NotFound();

        _context.Categories.Remove(category);

        var result = await _context.SaveChangesAsync() > 0;
        
        if(result) return Ok();

        return BadRequest(new ProblemDetails { Title = "Problem Deleting a category" });
    }

}