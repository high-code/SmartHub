using System;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using SmartHub.SaverService.DbEntities;
using SmartHub.SaverService.DTO;

namespace SmartHub.SaverService
{
  public class DbStorageService : IStorageService
  {

    private readonly IRepository<DbMeasurement> _measurementRepository;
    private readonly IRepository<DbStatus> _statusRepository;
    public DbStorageService(IRepository<DbMeasurement> measurementRepository,
      IRepository<DbStatus> statusRepository)
    {
      _measurementRepository = measurementRepository;
      _statusRepository = statusRepository;
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

      _measurementRepository.Add(dbMeasurement);
    }

    public void StoreStatus(string rawData)
    {
      var statusDto = JsonConvert.DeserializeObject<StatusDto>(rawData);

      var dbStatus = new DbStatus
      {
        DeviceId = statusDto.DeviceId,
        Status = statusDto.Status
      };

      _statusRepository.Add(dbStatus);
    }
  }
}

