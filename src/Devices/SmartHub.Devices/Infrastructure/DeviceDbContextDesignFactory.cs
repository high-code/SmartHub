using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using SmartHub.IntegrationEventLog;

namespace SmartHub.Devices.Infrastructure
{
  public class DeviceDbContextDesignFactory : IDesignTimeDbContextFactory<IntegrationEventLogContext>
  {
    public IntegrationEventLogContext CreateDbContext(string[] args)
    {
      var optionsBuilder = new DbContextOptionsBuilder<IntegrationEventLogContext>()
        .UseNpgsql("User ID = postgres;Password=admin;Server=localhost;Port=5432;Database=smarthub;Integrated Security=true; Pooling=true;", options =>
        {

          options.MigrationsAssembly("SmartHub.Infrastructure");
        });

      return new IntegrationEventLogContext(optionsBuilder.Options);
    }
  }
}
