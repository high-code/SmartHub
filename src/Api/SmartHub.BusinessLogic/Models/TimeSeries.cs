using System;
using System.Collections.Generic;
using System.Text;

namespace SmartHub.BL.Models
{
  public class TimeSeries
  {
    public IEnumerable<Measurement> Values { get; set; }
  }
}
