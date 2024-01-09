using Microsoft.EntityFrameworkCore;
using SiteDevice.DataLayer.Models;

namespace SiteDevice.DataLayer.Context;

public class DataContext : DbContext
{
    
    public DataContext(DbContextOptions options) 
        : base(options)
    {
    }

    public DbSet<Device> Devices { get; set; } = null!;
    public DbSet<User> Users { get; set; } = null!;
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Device>()
            .HasOne(e => e.User)
            .WithMany(e => e.Devices)
            .HasForeignKey(e => e.UserId)
            .IsRequired(false);
    }
}