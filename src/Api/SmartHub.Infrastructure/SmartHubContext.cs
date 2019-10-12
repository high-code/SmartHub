using Microsoft.EntityFrameworkCore;
using SmartHub.Infrastructure.Configurations;
using SmartHub.Infrastructure.Entities;

namespace SmartHub.Infrastructure
{
    public class SmartHubContext : DbContext
    {

      public DbSet<Device> Devices { get; set; }

      public DbSet<Measurement> Telemetries { get; set; }


    public SmartHubContext(DbContextOptions<SmartHubContext> options) : base(options) { }

      protected override void OnModelCreating(ModelBuilder modelBuilder)
      {
        
        modelBuilder.ApplyConfiguration(new DeviceConfiguration());
        modelBuilder.ApplyConfiguration(new MeasurementConfiguration());
        base.OnModelCreating(modelBuilder);
      }
    }
}
