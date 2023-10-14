using Microsoft.EntityFrameworkCore;
using Pennypal.Entities;

namespace Pennypal.Data;

public class AppDbContext: DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options): base(options)
    {
        
    }

    public DbSet<Category> Categories { get; set; }
    public DbSet<Expense> Expenses { get; set; }
    public DbSet<Report> Reports { get; set; }
}