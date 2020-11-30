using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using MediatR;
using SmartHub.Edge.Application.DTO;

namespace SmartHub.Edge.Application.Commands
{
  [DataContract]
  public class RecordMeasurementsCommand : IRequest<bool>
  {
    [DataMember]
    private readonly List<MeasurementDTO> _measurements;
    [DataMember]
    public IEnumerable<MeasurementDTO> Measurements => _measurements;

    public RecordMeasurementsCommand(List<MeasurementDTO> measurements)
    {
      _measurements = measurements;
    }
  }
}
