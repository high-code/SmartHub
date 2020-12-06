using Microsoft.EntityFrameworkCore;
using SmartHub.Edge.Infrastructure.Configuration;
using SmartHub.DataAccess;
using SmartHub.Edge.Domain.MeasurementAggregate;
using Microsoft.EntityFrameworkCore.Design;

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

  public class EdgeDbContextDesignFactory : IDesignTimeDbContextFactory<EdgeDbContext>
  {
    public EdgeDbContext CreateDbContext(string[] args)
    {
      var optionsBuilder = new DbContextOptionsBuilder<EdgeDbContext>()
        .UseNpgsql("Server =.; Database = smarthub_edge; Integrated Security = true;");
      return new EdgeDbContext(optionsBuilder.Options);
    }
  }
}
