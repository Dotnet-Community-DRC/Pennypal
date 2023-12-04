using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Pennypal.Persistence.Data;

public class AppDbContext: IdentityDbContext<AppUser, AppRole, string>
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
        
    }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Expense> Expenses { get; set; }
    public DbSet<Report> Reports { get; set; }
}