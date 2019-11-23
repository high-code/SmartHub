using System;
using SmartHub.DataAccess.Concrete;
using SmartHub.Edge.Infrastructure.Contracts;
using SmartHub.Edge.Infrastructure.Entities;

namespace SmartHub.Edge.Infrastructure.Concrete
{
  public class MeasurementRepository : Repository<Measurement>,IMeasurementsRepository
  {
    public MeasurementRepository(EdgeDbContext context) : base(context)
    {
    }

  }
}
