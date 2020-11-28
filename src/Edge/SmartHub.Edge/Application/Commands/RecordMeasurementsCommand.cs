using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using MediatR;

namespace SmartHub.Edge.Application.Commands
{
  [DataContract]
  public class RecordMeasurementsCommand : IRequest<bool>
  {
    [DataMember]
    private readonly List<MeasurementDto> _measurements;
    [DataMember]
    public IEnumerable<MeasurementDto> Measurements => _measurements;

    public RecordMeasurementsCommand(List<MeasurementDto> measurements)
    {
      _measurements = measurements;
    }

    public class MeasurementDto
    {
      public int DeviceId { get; set; }

      public DateTime DtSent { get; set; }

      public int MeasurementType { get; set; }

      public double Value { get; set; }

    }
  }
}
