using Autofac;
using MediatR;
using SmartHub.Edge.Application.Commands;
using SmartHub.Edge.Domain;
using SmartHub.Edge.Domain.MeasurementAggregate;
using SmartHub.Edge.Infrastructure.Concrete;
using System.Reflection;

namespace SmartHub.Edge.Infrastructure
{
  public class ApplicationModule : Autofac.Module
  {

    protected override void Load(ContainerBuilder builder)
    {

      builder.RegisterType<UnitOfWork>()
        .As<IUnitOfWork>()
        .InstancePerLifetimeScope();

      builder.RegisterType<MeasurementRepository>()
        .As<IMeasurementsRepository>()
        .InstancePerLifetimeScope();

      base.Load(builder);
    }
  }

}
