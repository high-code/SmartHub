using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using SmartHub.Edge.Domain.MeasurementAggregate;
using MediatR;
using SmartHub.Edge.Domain;
using SmartHub.Edge.Application.IntegrationEvents.Events;
using SmartHub.Edge.Application.IntegrationEvents;
using System.Linq;

namespace SmartHub.Edge.Application.Commands
{
  public class RecordMeasurementsCommandHandler : IRequestHandler<RecordMeasurementsCommand,bool>
  {
    private readonly IUnitOfWork _unitOfWork;
    private readonly IEdgeIntegrationEventLogService _edgeIntegrationEventLogService;

    public RecordMeasurementsCommandHandler(IUnitOfWork unitOfWork, IEdgeIntegrationEventLogService edgeIntegrationEventLogService)
    {
      _unitOfWork = unitOfWork;
      _edgeIntegrationEventLogService = edgeIntegrationEventLogService;
    }

    public async Task<bool> Handle(RecordMeasurementsCommand request, CancellationToken cancellationToken)
    {
      var fmeasurement = request.Measurements.First();
      var measurementReceivedIntegrationEvent = new MeasurementReceivedIntegrationEvent(fmeasurement.DeviceId, fmeasurement.DtSent,
                                                                                               fmeasurement.MeasurementType, fmeasurement.Value);
      await _edgeIntegrationEventLogService.AddEventsAsync(measurementReceivedIntegrationEvent);

      foreach (var receivedMeasurement in request.Measurements)
      {
        var measurement = new Measurement(receivedMeasurement.DeviceId, receivedMeasurement.DtSent,
          receivedMeasurement.Value, receivedMeasurement.MeasurementType);

        _unitOfWork.Measurements.Add(measurement);

      }

      var result = _unitOfWork.Commit();

      return (result == 0) ? false : true;
    }
  }
}
