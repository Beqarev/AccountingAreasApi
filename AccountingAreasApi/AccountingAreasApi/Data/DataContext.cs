using AccountingAreasApi.Models;
using Microsoft.EntityFrameworkCore;

namespace AccountingAreasApi.Data;

public class DataContext : DbContext
{
    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {
        
    }

    public DbSet<Area> Areas { get; set; }
    public DbSet<Region> Regions { get; set; }
    
}