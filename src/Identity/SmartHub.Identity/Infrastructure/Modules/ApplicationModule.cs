using Autofac;
using SmartHub.DataAccess.Concrete;
using SmartHub.DataAccess.Contracts;
using SmartHub.Identity.Application.IntegrationEvents;
using SmartHub.Identity.Infrastructure.Entities;
using SmartHub.Messaging.Abstractions;
using System.Reflection;

namespace SmartHub.Identity.Infrastructure.Modules
{
  public class ApplicationModule : Autofac.Module
  {

    protected override void Load(ContainerBuilder builder)
    {
      builder.RegisterAssemblyTypes(typeof(DeviceRegisteredIntegrationEventHandler).GetTypeInfo().Assembly)
      .AsClosedTypesOf(typeof(IIntegrationEventHandler<>));

      builder.Register(t => {
        var context = t.Resolve<IdentityServerDbContext>();
        return new Repository<IdentityServerDbContext, IdentityServerClient>(context);
        })
        .As<IRepository<IdentityServerDbContext, IdentityServerClient>>()
        .InstancePerLifetimeScope();

      base.Load(builder);
    }
  }
}
