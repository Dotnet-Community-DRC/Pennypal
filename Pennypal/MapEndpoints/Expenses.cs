namespace Pennypal.MapEndpoints;

public static class Expenses
{
    public static RouteGroupBuilder MapExpenses(this IEndpointRouteBuilder routes)
    {
        var app = routes.MapGroup("/expenses");
        
        // app.MapGet("/", async (AppDbContext context, IMapper mapper) =>
        // {
        //     var expenses = await context.Expenses.Include(e => e.Category).ToListAsync();
        //     return Results.Ok(mapper.Map<List<ExpenseDto>>(expenses));
        // });
        //
        // app.MapGet("/{id}", async (Guid id, AppDbContext context, IMapper mapper) =>
        // {
        //     var expense = await context.Expenses.Include(e => e.Category).FirstOrDefaultAsync(e => e.Id == id);
        //     return expense is null ? Results.NotFound("We could not find the Expense!") : Results.Ok(mapper.Map<ExpenseDto>(expense));
        // });
        //
        // app.MapGet("/status/{status}", async  (string status, AppDbContext context, IMapper mapper) =>
        // {
        //     if (!Enum.TryParse<Status>(status, out var statusEnum))
        //         return Results.NotFound($"Invalid status value: {status}");
        //     var expenses = await context.Expenses
        //         .Where(e => e.Status == statusEnum)
        //         .Include(e => e.Category)
        //         .ToListAsync();
        //     
        //     return Results.Ok(mapper.Map<List<ExpenseDto>>(expenses));
        // });
        //
        // app.MapGet("/currency/{currency}", async (string currency, AppDbContext context, IMapper mapper) =>
        // {
        //     if (!Enum.TryParse<Currency>(currency, out var currencyEnum))
        //         return Results.NotFound($"Invalid currency value: {currency}");
        //     var expenses = await context.Expenses
        //         .Where(e => e.Currency == currencyEnum)
        //         .Include(e => e.Category)
        //         .ToListAsync();
        //     
        //     return Results.Ok(mapper.Map<List<ExpenseDto>>(expenses));
        // });
        //
        // app.MapGet("/category/{categoryName}", async (string categoryName, AppDbContext context, IMapper mapper) =>
        // {
        //     var expenses = await context.Expenses
        //         .Where(e => e.Category.Name == categoryName)
        //         .Include(e => e.Category)
        //         .ToListAsync();
        //     return Results.Ok(mapper.Map<List<ExpenseDto>>(expenses));
        // });
        //
        // app.MapGet("/by-date", async (DateTime startDate, DateTime endDate, AppDbContext context, IMapper mapper) =>
        // {
        //     var filteredExpenses = await context.Expenses
        //         .Where(expense => expense.Date >= startDate && expense.Date <= endDate)
        //         .Include(e => e.Category)
        //         .ToListAsync();
        //
        //     return Results.Ok(mapper.Map<List<ExpenseDto>>(filteredExpenses));
        // });
        //
        // app.MapPost("/", async Task<Created<Expense>> ([FromBody] CreateExpenseDto expenseDto, 
        //     AppDbContext context, IMapper mapper) =>
        // {
        //     var expense = mapper.Map<Expense>(expenseDto);
        //     
        //     // Manually map CategoryId from CategoryName
        //     var category = await context.Categories.FirstOrDefaultAsync(c => c.Name == expenseDto.CategoryName);
        //     if (category is not null)
        //     {
        //         expense.CategoryId = category.Id;
        //     }
        //     else
        //     {
        //         throw new InvalidOperationException($"Category with name '{expenseDto.CategoryName}' not found.");
        //     }
        //     
        //     await context.Expenses.AddAsync(expense);
        //     await context.SaveChangesAsync();
        //     
        //     return TypedResults.Created($"/expenses/{expense.Id}", expense);
        // });
        //
        // app.MapPut("/{id}", async Task<Results<Ok, NotFound>> (Guid id, [FromBody] UpdateExpenseDto expenseDto, AppDbContext context) =>
        // {
        //     var expense = await context.Expenses.Include(e => e.Category).FirstOrDefaultAsync(e => e.Id == id);
        //     if (expense is null) 
        //         return TypedResults.NotFound();
        //     
        //     expense.Name = expenseDto.Name ?? expense.Name;
        //     expense.Amount = expenseDto.Amount;
        //     expense.Date = expenseDto.Date;
        //     expense.Description = expenseDto.Description;
        //     expense.Category.Name = expenseDto.CategoryName;
        //     
        //     await context.SaveChangesAsync();
        //     return TypedResults.Ok();
        // });
        //
        // app.MapDelete("/{id}", async Task<Results<NoContent, NotFound>> (Guid id, AppDbContext context) =>
        // {
        //     var expense = await context.Expenses.FindAsync(id);
        //     if (expense is null) return TypedResults.NotFound();
        //     context.Expenses.Remove(expense);
        //     await context.SaveChangesAsync();
        //     
        //     return TypedResults.NoContent();
        // });
        
        return app;
    }
}