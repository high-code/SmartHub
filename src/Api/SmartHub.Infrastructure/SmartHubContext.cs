using System.Runtime.InteropServices.ComTypes;
using Microsoft.EntityFrameworkCore;
using SmartHub.Domain.Configurations;
using SmartHub.Domain.Entities;

namespace SmartHub.Domain
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
