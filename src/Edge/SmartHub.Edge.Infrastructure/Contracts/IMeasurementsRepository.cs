using System;
using SmartHub.DataAccess.Contracts;
using SmartHub.Edge.Infrastructure.Entities;

namespace SmartHub.Edge.Infrastructure.Contracts
{
  public interface IMeasurementsRepository : IRepository<Measurement>
  {
  }
}
