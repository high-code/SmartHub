using System;
using SmartHub.DataAccess.Contracts;

namespace SmartHub.Edge.Domain.MeasurementAggregate
{
  public interface IMeasurementsRepository : IRepository<Measurement>
  {
  }
}
