using System;
using Microsoft.Extensions.DependencyInjection;

namespace SmartHub.SaverService
{
  public class DbSaverService
  {

    private readonly IServiceProvider _serviceProvider;

    public DbSaverService(IServiceProvider serviceProvider)
    {
      _serviceProvider = serviceProvider;
    }

    public void SaveData(Telemetry telemetry)
    {
      using (var scope = _serviceProvider.CreateScope())
      {
        var telemetryRepo = scope.ServiceProvider.GetService<ITelemetryRepository>();

        telemetryRepo.Add(telemetry);
      }
    }
  }
}
