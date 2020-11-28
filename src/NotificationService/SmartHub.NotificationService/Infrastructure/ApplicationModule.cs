using Autofac;
using SmartHub.Messaging.Abstractions;
using SmartHub.NotificationService.IntegrationEvents.EventHandling;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace SmartHub.NotificationService.Infrastructure
{
  public class ApplicationModule : Autofac.Module
  {
    public ApplicationModule()
    {

    }
    protected override void Load(ContainerBuilder builder)
    {


      builder.RegisterAssemblyTypes(typeof(MeasurementReceivedIntegrationEventHandler).GetTypeInfo().Assembly)
            .AsClosedTypesOf(typeof(IIntegrationEventHandler<>));
    }
  }
}
