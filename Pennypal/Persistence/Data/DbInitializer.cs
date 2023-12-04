namespace Pennypal.Persistence.Data;

public class DbInitializer
{
    public static async Task InitDb(WebApplication app)
    {
        try
        {
            using var scope = app.Services.CreateScope();
            var context = scope.ServiceProvider.GetService<AppDbContext>();
            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<AppUser>>();
            await SeedDataAsync(context, userManager);
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message, "Error While seeding data");
            throw;
        }
    }
    private static async Task SeedDataAsync(AppDbContext context, UserManager<AppUser> userManager)
    {
        await context.Database.MigrateAsync();
        
        if (!context.Categories.Any() || 
            !context.Expenses.Any() || 
            !userManager.Users.Any())
        {
            var users = new List<AppUser>
        {
            new AppUser
            {
                DisplayName = "Sudi",
                UserName = "sudi",
                Email = "contact@sudi.dev"
            },
            new AppUser
            {
                DisplayName = "Simba",
                UserName = "simba",
                Email = "sudimayenge@gmail.com"
            },
            new AppUser
            {
                DisplayName = "Sarah",
                UserName = "sarah",
                Email = "sudisimbadav@gmail.com"
            },
        };

        foreach (var user in users)
        {
            await userManager.CreateAsync(user, "Pa$$w0rd");
        }
        
        var rand = new Random();
        const int maxDays = 365 * 1;
        
        var randomDate = DateTime.UtcNow.AddDays(-rand.Next(maxDays));
        
        var categories = new List<Category>
        {
            new Category { Id = Guid.Parse("6baec43a-6769-4a3f-8a17-74e617258f76"), Name = "Electronics", Description = "Devices and equipment powered by electricity." },
            new Category { Id = Guid.Parse("4b24a045-1c3d-4e2d-93a3-cd59bb8a7c0e"), Name = "Clothing", Description = "Apparel and garments for various purposes." },
            new Category { Id = Guid.Parse("2f6c1836-5b6b-4080-9a2b-732a799ce5f0"), Name = "Furniture", Description = "Functional and decorative items for interior spaces." },
            new Category { Id = Guid.Parse("a488f89a-bc7c-4b6d-81a3-853c99fc4e72"), Name = "Books", Description = "Written or printed works for reading and learning." },
            new Category { Id = Guid.Parse("f96d23c0-8910-4f45-b36d-86b0144d3035"), Name = "Sports", Description = "Equipment and activities related to physical exercise." },
            new Category { Id = Guid.Parse("63ae5389-40a1-4b68-9a07-d5b7f0c51cc2"), Name = "Beauty", Description = "Products and services related to personal care and appearance." },
            new Category { Id = Guid.Parse("8c53b0e7-1c11-41d7-90f3-8db874d12963"), Name = "Home Decor", Description = "Items to enhance and decorate living spaces." },
            new Category { Id = Guid.Parse("e35cb3f6-17c9-4a6a-88f1-597f39df5c56"), Name = "Toys", Description = "Playthings and entertainment for children." },
            new Category { Id = Guid.Parse("e574a2d6-7cfd-4f80-912f-072551285d16"), Name = "Automotive", Description = "Products and services related to vehicles." },
            new Category { Id = Guid.Parse("41bb24b5-9876-4bd9-98f6-729f8b1ecbc2"), Name = "Kitchenware", Description = "Utensils and appliances for cooking and dining." },
            new Category { Id = Guid.Parse("3b71d982-106e-49de-9d72-0175c4932f9e"), Name = "Gardening", Description = "Tools and plants for gardening and landscaping." },
            new Category { Id = Guid.Parse("9dfef09b-fab7-4b3b-80f0-37d6df878fd9"), Name = "Health", Description = "Products and services related to well-being and medical care." },
            new Category { Id = Guid.Parse("2af8c0ef-4656-41d3-9eaa-01a9037d2c87"), Name = "Stationery", Description = "Supplies for writing, drawing, and office use." },
            new Category { Id = Guid.Parse("72bf97b9-6c66-463f-85f4-1b4ce2c0a56e"), Name = "Music", Description = "Instruments, recordings, and musical accessories." },
            new Category { Id = Guid.Parse("f1258356-1847-4bd3-8e4e-4dcfd1956867"), Name = "Pets", Description = "Supplies and services for pet care." },
            new Category { Id = Guid.Parse("e7d9349e-05a3-4db9-94b0-0d27a82eaac1"), Name = "Jewelry", Description = "Ornaments and accessories made of precious metals and gems." },
            new Category { Id = Guid.Parse("9dd1d053-5a3a-4a9c-a93b-461ac9c30f56"), Name = "Art", Description = "Visual and decorative creations in various forms." },
            new Category { Id = Guid.Parse("43e3e98e-2f2f-4a16-bfa5-c86285744ac0"), Name = "Outdoors", Description = "Gear and equipment for outdoor activities." },
            new Category { Id = Guid.Parse("d2920192-0a87-4a3f-b27c-9e3776673f75"), Name = "Travel", Description = "Products and services for traveling and exploration." },
            new Category { Id = Guid.Parse("9f3436c1-3997-4860-83ef-6a54539f28e6"), Name = "Electrical", Description = "Electrical equipment and components." }
        };
        context.AddRange(categories);
        
        var expenses = new List<Expense>
        {
            new Expense { Id = Guid.Parse(Guid.NewGuid().ToString()), Name = "Expense 21", Amount = 150.30M, Date = randomDate, Description = "Expense description 21", CategoryId = Guid.Parse("2f6c1836-5b6b-4080-9a2b-732a799ce5f0"), Currency = Currency.Usd, Status = Status.Approved},
            new Expense { Id = Guid.Parse(Guid.NewGuid().ToString()), Name = "Expense 22", Amount = 75.20M, Date =  randomDate, Description = "Expense description 22", CategoryId = Guid.Parse("a488f89a-bc7c-4b6d-81a3-853c99fc4e72"), Currency = Currency.Usd, Status = Status.Approved},
            new Expense { Id = Guid.Parse(Guid.NewGuid().ToString()), Name = "Expense 21", Amount = 150.30M, Date = randomDate, Description = "Expense description 21", CategoryId = Guid.Parse("2f6c1836-5b6b-4080-9a2b-732a799ce5f0"), Currency = Currency.Usd, Status = Status.Approved},
            new Expense { Id = Guid.Parse(Guid.NewGuid().ToString()), Name = "Expense 22", Amount = 75.20M, Date =  randomDate, Description = "Expense description 22", CategoryId = Guid.Parse("a488f89a-bc7c-4b6d-81a3-853c99fc4e72"), Currency = Currency.Usd, Status = Status.Approved},
            new Expense { Id = Guid.Parse(Guid.NewGuid().ToString()), Name = "Expense 23", Amount = 300.00M, Date = randomDate, Description = "Expense description 23", CategoryId = Guid.Parse("63ae5389-40a1-4b68-9a07-d5b7f0c51cc2"), Currency = Currency.Usd, Status = Status.Approved},
            new Expense { Id = Guid.Parse(Guid.NewGuid().ToString()), Name = "Expense 24", Amount = 50.70M, Date =  randomDate, Description = "Expense description 24", CategoryId = Guid.Parse("f96d23c0-8910-4f45-b36d-86b0144d3035"), Currency = Currency.Gbp, Status = Status.Approved},
            new Expense { Id = Guid.Parse(Guid.NewGuid().ToString()), Name = "Expense 25", Amount = 120.00M, Date = randomDate, Description = "Expense description 25", CategoryId = Guid.Parse("8c53b0e7-1c11-41d7-90f3-8db874d12963"), Currency = Currency.Usd, Status = Status.Approved},
            new Expense { Id = Guid.Parse(Guid.NewGuid().ToString()), Name = "Expense 26", Amount = 200.15M, Date = randomDate, Description = "Expense description 26", CategoryId = Guid.Parse("e35cb3f6-17c9-4a6a-88f1-597f39df5c56"), Currency = Currency.Jpy, Status = Status.Approved},
            new Expense { Id = Guid.Parse(Guid.NewGuid().ToString()), Name = "Expense 27", Amount = 80.45M, Date =  randomDate, Description = "Expense description 27", CategoryId = Guid.Parse("e574a2d6-7cfd-4f80-912f-072551285d16"), Currency = Currency.Usd, Status = Status.Approved},
            new Expense { Id = Guid.Parse(Guid.NewGuid().ToString()), Name = "Expense 28", Amount = 95.90M, Date =  randomDate, Description = "Expense description 28", CategoryId = Guid.Parse("41bb24b5-9876-4bd9-98f6-729f8b1ecbc2"), Currency = Currency.Eur, Status = Status.Approved},
            new Expense { Id = Guid.Parse(Guid.NewGuid().ToString()), Name = "Expense 29", Amount = 170.60M, Date = randomDate, Description = "Expense description 29", CategoryId = Guid.Parse("3b71d982-106e-49de-9d72-0175c4932f9e"), Currency = Currency.Rwf, Status = Status.Rejected},        
            new Expense { Id = Guid.Parse(Guid.NewGuid().ToString()), Name = "Expense 30", Amount = 40.00M, Date =  randomDate, Description = "Expense description 30", CategoryId = Guid.Parse("9dfef09b-fab7-4b3b-80f0-37d6df878fd9"), Currency = Currency.Cdf, Status = Status.Submitted}
        };
        
        context.Expenses.AddRange(expenses);
        await context.SaveChangesAsync();
        
        }
        else
        {
            Console.WriteLine("Already have data - no need to seed db");
        }
    }
}