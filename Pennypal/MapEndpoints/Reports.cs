namespace Pennypal.MapEndpoints;

public static class Reports
{
    public static RouteGroupBuilder MapReports(this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/reports");

        // group.MapGet("/", async(AppDbContext context) => await context.Reports.ToListAsync());
        //
        // group.MapGet("/{id}", async Task<Results<Ok<Report>, NotFound>> (Guid id, AppDbContext context) =>
        // {
        //     var report = await context.Reports.FindAsync(id);
        //     if (report is null) return TypedResults.NotFound();
        //
        //     return TypedResults.Ok(report);
        // });
        
        return group;
    }
}