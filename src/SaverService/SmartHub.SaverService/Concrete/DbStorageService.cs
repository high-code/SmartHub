using System;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using SmartHub.SaverService.Concrete;
using SmartHub.SaverService.DbEntities;
using SmartHub.SaverService.DTO;

namespace SmartHub.SaverService
{
  public class DbStorageService : IStorageService
  {

    private readonly IServiceProvider _serviceProvider;

    public DbStorageService(IServiceProvider serviceProvider)
    {
      _serviceProvider = serviceProvider;

    }

    public void StoreMeasurements(string rawData)
    {
      var measurementDto = JsonConvert.DeserializeObject<MeasurementDto>(rawData);

      var dbMeasurement = new DbMeasurement
      {
        DeviceId = measurementDto.DeviceId,
        Type = measurementDto.Type,
        Value = measurementDto.Value,
        DtSend = measurementDto.DtSend
      };

      using (var scope = _serviceProvider.CreateScope())
      {
        var measurementRepository = scope.ServiceProvider.GetRequiredService<IRepository<DbMeasurement>>();
        measurementRepository.Add(dbMeasurement);
      }
      
    }

    public void StoreStatus(string rawData)
    {
      var statusDto = JsonConvert.DeserializeObject<StatusDto>(rawData);

      var dbStatus = new DbStatus
      {
        DeviceId = statusDto.DeviceId,
        Status = statusDto.Status
      };

      using (var scope = _serviceProvider.CreateScope())
      {
        var statusRepository = scope.ServiceProvider.GetRequiredService<IRepository<DbStatus>>();
        statusRepository.Add(dbStatus);
      }

    }
  }
}

