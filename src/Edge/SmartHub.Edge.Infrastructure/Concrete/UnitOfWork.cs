using System;
using SmartHub.DataAccess.Concrete;
using SmartHub.Edge.Infrastructure.Contracts;

namespace SmartHub.Edge.Infrastructure.Concrete
{
  public class UnitOfWork : UnitOfWorkBase<EdgeDbContext>, IUnitOfWork
  {

    public IMeasurementsRepository Measurements { get; }

    public UnitOfWork(EdgeDbContext dbContext, IMeasurementsRepository measurementsRepository) : base(dbContext)
    {
      Measurements = measurementsRepository;
    }
  }
}
