using Microsoft.EntityFrameworkCore;
using SmartHub.DataAccess;
using SmartHub.Infrastructure.Configurations;
using SmartHub.Infrastructure.Entities;

namespace SmartHub.Infrastructure
{
  public class SmartHubContext : DbContext
  {

    public DbSet<Device> Devices { get; set; }

    public SmartHubContext(DbContextOptions<SmartHubContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      base.OnModelCreating(modelBuilder);
      modelBuilder.ApplyConfiguration(new DeviceConfiguration());

      modelBuilder.ToSnakeCaseNamingConventions();
    }
  }
}
