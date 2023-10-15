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
    public async Task<ActionResult<Category>> CreateCategory(CreateCategoryDto categoryDto)
    {
        _logger.LogInformation("Creating new category: {@categoryDto}", categoryDto);
        var category = _mapper.Map<Category>(categoryDto);

        _context.Categories.Add(category);

        var result = await _context.SaveChangesAsync() > 0;

        if (result) return CreatedAtRoute("GetCategory", new { Id = category.Id }, category);
        
        _logger.LogWarning("Problem saving category.");
        
        return BadRequest(new ProblemDetails { Title = "Problem saving Category" });
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<Category>> UpdateCategory(Guid id, UpdateCategoryDto categoryDto)
    {
        var category = await _context.Categories.FindAsync(id);

        if (category is null) return NotFound();

        // _mapper.Map(categoryDto, category);
        category.Name = categoryDto.Name ?? category.Name;
        category.Description = categoryDto.Description ?? category.Description;

        var result = await _context.SaveChangesAsync() > 0;

        if (result) return Ok(category);

        return BadRequest(new ProblemDetails { Title = "Problem Updating the Category" });
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteCategory(Guid id)
    {
        var category = await _context.Categories.FindAsync(id);

        if (category is null) return NotFound("Could not be found!");

        _context.Categories.Remove(category);

        var result = await _context.SaveChangesAsync() > 0;
        
        if(result) return Ok();

        return BadRequest(new ProblemDetails { Title = "Problem Deleting a category" });
    }

}