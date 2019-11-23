using System;
using SmartHub.DataAccess.Contracts;

namespace SmartHub.Edge.Infrastructure.Contracts
{
  public interface IUnitOfWork : IUnitOfWorkBase
  {
     IMeasurementsRepository Measurements { get; }
  }
}
