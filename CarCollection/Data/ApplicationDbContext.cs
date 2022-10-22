using CarCollection.Data;
using Microsoft.EntityFrameworkCore;
using BugTracker.Data;

public class ApplicationDbContext : DbContext
    {
    public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

    public DbSet<Vehicle> Vehicles { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        modelBuilder.Entity<Vehicle>().HasData(
            new Vehicle
                {
                Id = 1,
                Name = "F-150",
                Year = 2005,
                Description = "usce quis sem vel ante scelerisque eleifend nec vel ante. Fusce vel dictum tellus.",
                Address = "Orlando, FL",
                Engine = "V8"
                },
            new Vehicle
                {
                Id = 2,
                Name = "F-150",
                Year = 2005,
                Description = "usce quis sem vel ante scelerisque eleifend nec vel ante. Fusce vel dictum tellus.",
                Address = "Orlando, FL",
                Engine = "V8"
                },
            new Vehicle
                {
                Id = 3,
                Name = "F-150",
                Year = 2005,
                Description = "usce quis sem vel ante scelerisque eleifend nec vel ante. Fusce vel dictum tellus.",
                Address = "Orlando, FL",
                Engine = "V8"
                }
            );
        }
    public DbSet<BugTracker.Data.Comment> Comment { get; set; }
    }