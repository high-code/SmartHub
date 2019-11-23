using Microsoft.EntityFrameworkCore;
using SmartHub.Edge.Infrastructure.Configuration;
using SmartHub.Edge.Infrastructure.Entities;
using SmartHub.DataAccess;

namespace SmartHub.Edge.Infrastructure
{
  public class EdgeDbContext : DbContext
  {
     public DbSet<Measurement> Measurements { get; set; }


    public EdgeDbContext(DbContextOptions<EdgeDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
      base.OnModelCreating(builder);
      builder.ApplyConfiguration(new MeasurementConfiguration());

      builder.ToSnakeCaseNamingConventions();
    }
 
  }
}
