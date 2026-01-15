

using Microsoft.EntityFrameworkCore;

namespace StainlessMarketApi.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<Entities.StokProductEntities> StokProducts { get; set; }
    public DbSet<Entities.FasonProductEntity> FasonProducts { get; set; }
}