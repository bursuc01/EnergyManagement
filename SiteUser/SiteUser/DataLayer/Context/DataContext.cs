using Microsoft.EntityFrameworkCore;
using SiteUser.DataLayer.Models;

namespace SiteUser.DataLayer.Context;

public class DataContext : DbContext
{
    
    public DataContext(DbContextOptions options) 
        : base(options)
    {
    }

    public DbSet<User> Users { get; set; } = null!;
}