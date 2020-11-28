using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Autofac;
using MediatR;
using SmartHub.Edge.Application;
using SmartHub.Edge.Application.Commands;

namespace SmartHub.Edge.Infrastructure
{
  public class MediatorModule : Autofac.Module
  {

    public MediatorModule()
    {

    }
    protected override void Load(ContainerBuilder builder)
    {

      builder.RegisterAssemblyTypes(typeof(IMediator).GetTypeInfo().Assembly)
        .AsImplementedInterfaces();

      builder.Register<ServiceFactory>(context =>
      {
        var componentContext = context.Resolve<IComponentContext>();
        return t => { object o; return componentContext.TryResolve(t, out o) ? o : null; };
      });

      builder.RegisterAssemblyTypes(typeof(RecordMeasurementsCommand).GetTypeInfo().Assembly)
        .AsClosedTypesOf(typeof(IRequestHandler<,>));
      builder.RegisterGeneric(typeof(TransactionBehavior<,>)).As(typeof(IPipelineBehavior<,>));

    }
  }
}
