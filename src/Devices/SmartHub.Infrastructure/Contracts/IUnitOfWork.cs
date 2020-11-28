using System;
using SmartHub.DataAccess.Contracts;

namespace SmartHub.Infrastructure.Contracts
{
  public interface IUnitOfWork : IUnitOfWorkBase
  {
    IDeviceRepository Devices { get; }
  }
}
