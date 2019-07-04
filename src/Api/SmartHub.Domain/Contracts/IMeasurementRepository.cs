using System;
using System.Collections.Generic;
using SmartHub.Domain.Entities;

namespace SmartHub.Domain.Contracts
{
  public interface IMeasurementRepository : IRepository<Measurement>
  {
    IEnumerable<Measurement> GetFromDateRange(DateTime? from, DateTime? to);
  }
}
