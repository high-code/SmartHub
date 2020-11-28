using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using MediatR.Pipeline;
using Microsoft.EntityFrameworkCore;
using SmartHub.IntegrationEventLog.Services;

namespace SmartHub.MediatR
{
  public class TransactionBehavior<TContext, TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TContext : DbContext
  {

    private readonly DbContext _dbContext;
    private readonly IIntegrationEventService _integrationEventService;

    public TransactionBehavior(DbContext context, IIntegrationEventService integrationEventService)
    {
      _dbContext = context;
      _integrationEventService = integrationEventService;
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

          Guid transactionId;

          using(var transaction = await _dbContext.Database.BeginTransactionAsync())
          {

            response = await next();

            try
            {
              await _dbContext.SaveChangesAsync();
              transaction.Commit();
              transactionId = transaction.TransactionId;
            }
            catch(Exception ex)
            {
              transaction.Rollback();
            }
          }

          await _integrationEventService.PublishAsync(transactionId);
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
