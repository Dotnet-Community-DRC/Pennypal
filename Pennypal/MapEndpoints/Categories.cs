namespace Pennypal.MapEndpoints;

public static class Categories
{
    public static RouteGroupBuilder MapCategories(this IEndpointRouteBuilder routes)
    {
        var app = routes.MapGroup("/categories");
        
        // app.MapGet("/", async (AppDbContext context) =>
        // {
        //     var categories = await context.Categories.ToListAsync();
        //     return Results.Ok(categories);
        // });
        //
        // app.MapGet("/{id}", async Task<Results<Ok<Category>, NotFound>> (Guid id, AppDbContext context) =>
        // {
        //     var category = await context.Categories.FindAsync(id);
        //     return category is null ? TypedResults.NotFound() : TypedResults.Ok(category);
        // });
        //
        // app.MapPost("/", async Task<Created<Category>> ([FromBody] CreateCategoryDto categoryDto, 
        //     AppDbContext context, IMapper mapper) =>
        // {
        //     var category = mapper.Map<Category>(categoryDto);
        //     await context.Categories.AddAsync(category);
        //     await context.SaveChangesAsync();
        //     return TypedResults.Created($"/categories/{category.Id}", category);
        // });
        //
        // app.MapPut("/{id}", async Task<Results<Ok<Category>, NotFound, BadRequest>> (Guid id, [FromBody] UpdateCategoryDto categoryDto,
        //     AppDbContext context) =>
        // {
        //     var category = await context.Categories.FindAsync(id);
        //     if (category is null)
        //         return TypedResults.NotFound();
        //     
        //     category.Name = categoryDto.Name ?? category.Name;
        //     category.Description = categoryDto.Description ?? category.Description;
        //     var result = await context.SaveChangesAsync() > 0;
        //     return result ? TypedResults.Ok(category) : TypedResults.BadRequest();
        // });
        //
        // app.MapDelete("/{id}", async Task<Results<NotFound, Ok, BadRequest>> (Guid id, AppDbContext context) =>
        // {
        //     var category = await context.Categories.FindAsync(id);
        //     if (category is null)
        //         return TypedResults.NotFound();
        //     
        //     context.Categories.Remove(category);
        //     var result = await context.SaveChangesAsync() > 0;
        //     return result ? TypedResults.Ok() : TypedResults.BadRequest();
        // });
        //
        return app;
    }
}