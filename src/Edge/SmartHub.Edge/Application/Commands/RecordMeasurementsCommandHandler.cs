using System;
using System.Threading;
using System.Threading.Tasks;
using SmartHub.Edge.Domain.MeasurementAggregate;
using MediatR;
using SmartHub.Edge.Domain;
using SmartHub.Edge.Application.IntegrationEvents.Events;
using SmartHub.Edge.Application.IntegrationEvents;
using System.Linq;
using SmartHub.Edge.Application.DTO;
using Microsoft.Extensions.Logging;

namespace SmartHub.Edge.Application.Commands
{
  public class RecordMeasurementsCommandHandler : IRequestHandler<RecordMeasurementsCommand,bool>
  {
    private readonly IUnitOfWork _unitOfWork;
    private readonly IEdgeIntegrationEventLogService _edgeIntegrationEventLogService;
    private readonly ILogger<RecordMeasurementsCommandHandler> _logger;
    public RecordMeasurementsCommandHandler(IUnitOfWork unitOfWork, IEdgeIntegrationEventLogService edgeIntegrationEventLogService,
                                               ILogger<RecordMeasurementsCommandHandler> logger)
    {
      _unitOfWork = unitOfWork;
      _edgeIntegrationEventLogService = edgeIntegrationEventLogService;
      _logger = logger;
    }

    public async Task<bool> Handle(RecordMeasurementsCommand request, CancellationToken cancellationToken)
    {
      _logger.LogInformation("Starting {command} handling", request);
      var dtReceived = DateTime.UtcNow;
      var measurementDTOs = request.Measurements.Select(m => new MeasurementDTO(m.DeviceId, m.DtSent, m.MeasurementType, m.Value, dtReceived));

      var measurementReceivedIntegrationEvent = new MeasurementReceivedIntegrationEvent(measurementDTOs);
      await _edgeIntegrationEventLogService.AddEventsAsync(measurementReceivedIntegrationEvent);


      var measurements = request.Measurements.Select(m => new Measurement(m.DeviceId,m.DtSent, dtReceived,
          m.Value,m.MeasurementType));

      _unitOfWork.Measurements.AddRange(measurements);
      var result = _unitOfWork.Commit();

      return (result == 0) ? false : true;
    }
  }
}
