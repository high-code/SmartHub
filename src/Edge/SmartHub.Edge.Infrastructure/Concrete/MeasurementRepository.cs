﻿using System;
using SmartHub.DataAccess.Concrete;
using SmartHub.Edge.Domain.MeasurementAggregate;

namespace SmartHub.Edge.Infrastructure.Concrete
{
  public class MeasurementRepository : Repository<EdgeDbContext, Measurement>,IMeasurementsRepository
  {
    public MeasurementRepository(EdgeDbContext context) : base(context)
    {
    }

  }
}
