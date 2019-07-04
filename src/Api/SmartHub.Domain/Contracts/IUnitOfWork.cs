using System;

namespace SmartHub.Domain.Contracts
{
  public interface IUnitOfWork : IDisposable
  {
    IDeviceRepository Devices { get; }

    IMeasurementRepository Measurements { get; }

    int Commit();
  }
}
