using System.Collections.Generic;

namespace SmartHub.BusinessLogic.Models
{
  public class TimeSeries
  {
    public IEnumerable<Measurement> Values { get; set; }
  }
}
