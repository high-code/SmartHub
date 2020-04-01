using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using SmartHub.Edge.Domain.MeasurementAggregate;
using MediatR;
using SmartHub.Edge.Domain;

namespace SmartHub.Edge.Application.Commands
{
  public class RecordMeasurementsCommandHandler : IRequestHandler<RecordMeasurementsCommand,bool>
  {
    private readonly IUnitOfWork _unitOfWork; 



    public RecordMeasurementsCommandHandler(IUnitOfWork unitOfWork)
    {
      _unitOfWork = unitOfWork;
    }

    public Task<bool> Handle(RecordMeasurementsCommand request, CancellationToken cancellationToken)
    {
      
      foreach(var receivedMeasurement in request.Measurements)
      {
        var measurement = new Measurement(receivedMeasurement.DeviceId, receivedMeasurement.DtSent,
          receivedMeasurement.Value, receivedMeasurement.MeasurementType);

        _unitOfWork.Measurements.Add(measurement);

      }

      var result = _unitOfWork.Commit();

      return Task.FromResult((result == 0) ? false : true);

    }
  }
}
