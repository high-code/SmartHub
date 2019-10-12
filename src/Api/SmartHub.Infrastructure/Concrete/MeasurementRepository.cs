using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using SmartHub.Domain.Contracts;
using SmartHub.Domain.Entities;

namespace SmartHub.Domain.Concrete
{
  public class MeasurementRepository : Repository<Measurement>, IMeasurementRepository
  {
    public MeasurementRepository(DbContext dbContext) : base(dbContext)
    {
    }

    public IEnumerable<Measurement> GetFromDateRange(DateTime? from, DateTime? to)
    {
      throw new NotImplementedException();
    }
  }
}
