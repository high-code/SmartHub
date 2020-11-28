using System;
using SmartHub.Edge.Domain.MeasurementAggregate;
using SmartHub.DataAccess.Contracts;

namespace SmartHub.Edge.Domain
{
  public interface IUnitOfWork : IUnitOfWorkBase
  {
     IMeasurementsRepository Measurements { get; }
  }
}
