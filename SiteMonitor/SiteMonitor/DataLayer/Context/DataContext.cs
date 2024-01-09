using Microsoft.EntityFrameworkCore;
using SiteMonitor.DataLayer.Models;

namespace SiteMonitor.DataLayer.Context;

public class DataContext: DbContext
{
    public DataContext(DbContextOptions options)
        : base(options)
    {
    }
    public DbSet<EnergyConsumption?> EnergyConsumptions { get; set; } = null!;
}