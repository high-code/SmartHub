using System;
using System.Collections.Generic;
using SmartHub.Infrastructure.Contracts;
using SmartHub.Infrastructure.Entities;

namespace SmartHub.Domain.Contracts
{
  public interface IMeasurementRepository : IRepository<Measurement>
  {
    IEnumerable<Measurement> GetFromDateRange(DateTime? from, DateTime? to);
  }
}
