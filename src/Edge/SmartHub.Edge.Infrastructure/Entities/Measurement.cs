using System;

namespace SmartHub.BusinessLogic.Models
{
  public class Measurement
  {



    public DateTime DtSent { get; set; }

    public double Value { get; set; }

    public MeasurementType Type { get; set; }

  }
}
