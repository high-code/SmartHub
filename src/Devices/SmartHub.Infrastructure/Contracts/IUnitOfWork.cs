using System;
using SmartHub.Domain.Contracts;

namespace SmartHub.Infrastructure.Contracts
{
  public interface IUnitOfWork : IDisposable
  {
    IDeviceRepository Devices { get; }

    IMeasurementRepository Measurements { get; }

    int Commit();
  }
}
