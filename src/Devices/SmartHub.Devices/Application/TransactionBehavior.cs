using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SmartHub.Devices.Application;
using SmartHub.Infrastructure;

namespace SmartHub.Edge.Application
{
  public class TransactionBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
  {

    private readonly DbContext _dbContext;
    private readonly IDeviceIntegrationEventLogService _deviceIntegrationEventService;

    public TransactionBehavior(SmartHubContext context, IDeviceIntegrationEventLogService integrationEventService)
    {
      _dbContext = context;
      _deviceIntegrationEventService = integrationEventService;
    }

    public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
    {

      var response = default(TResponse);

      try
      {

        if (_dbContext.Database.CurrentTransaction != null)
          return await next();

        var executionStrategy = _dbContext.Database.CreateExecutionStrategy();

        await executionStrategy.ExecuteAsync(async () =>
        {

          Guid transactionId = default(Guid);

          using (var transaction = await _dbContext.Database.BeginTransactionAsync())
          {

            response = await next();

            try
            {
              await _dbContext.SaveChangesAsync();
              transaction.Commit();
              transactionId = transaction.TransactionId;
            }
            catch (Exception ex)
            {
              transaction.Rollback();
            }
          }

          await _deviceIntegrationEventService.PublishEventsAsync(transactionId);
        });

      }
      catch (Exception ex)
      {
        Debug.Write("Exception thrown");
      }

      return response;

    }
  }
}
