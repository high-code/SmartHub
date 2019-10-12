using System;

namespace SmartHub.Domain.Entities
{
  public class Measurement
  {
    public int Id { get; set; }

    public int Type { get; set; }

    public double Value { get; set; }

    public DateTime? DtSend { get; set; }

    public Guid DeviceId { get; set; }

  }
}
