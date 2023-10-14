using Microsoft.EntityFrameworkCore;
using Pennypal.Entities;

namespace Pennypal.Data;

public class DbInitializer
{
    public static void InitDb(WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        SeedData(scope.ServiceProvider.GetService<AppDbContext>());
    }
    private static void SeedData(AppDbContext context)
    {
        context.Database.Migrate();
        
        if (context.Categories.Any())
        {
            Console.WriteLine("Already have data - no need to seed db");
            return;
        };
        
        var categories = new List<Category>
        {
            new Category { Guid = Guid.NewGuid(), Name = "Electronics", Description = "Devices and equipment powered by electricity." },
            new Category { Guid = Guid.NewGuid(), Name = "Clothing", Description = "Apparel and attire for various purposes." },
            new Category { Guid = Guid.NewGuid(), Name = "Home and Garden", Description = "Items related to household and gardening activities." },
            new Category { Guid = Guid.NewGuid(), Name = "Books", Description = "Printed and digital reading materials." },
            new Category { Guid = Guid.NewGuid(), Name = "Sports and Outdoors", Description = "Sporting goods and outdoor equipment." },
            new Category { Guid = Guid.NewGuid(), Name = "Toys and Games", Description = "Playthings and games for entertainment." },
            new Category { Guid = Guid.NewGuid(), Name = "Health and Wellness", Description = "Products and services related to health and well-being." },
            new Category { Guid = Guid.NewGuid(), Name = "Automotive", Description = "Vehicle-related products and services." },
            new Category { Guid = Guid.NewGuid(), Name = "Food and Beverage", Description = "Edible items and beverages for consumption." },
            new Category { Guid = Guid.NewGuid(), Name = "Furniture", Description = "Various types of indoor and outdoor furniture." }
        };
    }
}