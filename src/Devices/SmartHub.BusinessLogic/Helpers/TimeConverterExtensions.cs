using System;
using System.Collections.Generic;
using System.Text;

namespace SmartHub.BL.Helpers
{
  public static class TimeConverterExtensions
  {
    public static int ToUnixTime(this DateTime dateTime)
    {
      
      return (int)dateTime.Subtract(new DateTime(1970, 1, 1)).TotalSeconds;
    }

    public static DateTime FromUnixTime(long unixTime)
    {
      DateTime date = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
      return date.AddSeconds(unixTime).ToUniversalTime();
    }
  }
}
