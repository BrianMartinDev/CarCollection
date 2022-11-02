using BugTracker.Data;
using CarCollection.Data;
using CarCollection.Data.Configurations;
using Microsoft.EntityFrameworkCore;

public class ApplicationDbContext : DbContext
    {
    public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

    public DbSet<Vehicle> Vehicles { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        modelBuilder.ApplyConfiguration(new RoleConfiguration());
        modelBuilder.ApplyConfiguration(new VehicleConfiguration());
        }
    public DbSet<Comment> Comment { get; set; }
    }