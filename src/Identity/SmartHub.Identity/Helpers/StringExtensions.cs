using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SmartHub.Identity.Helpers
{
  public static class StringExtensions
  {
    public static string ToSnakeCaseInvariant(this string input)
    {
      var startUnderscore = Regex.Match(input, @"^_+");
      return startUnderscore + Regex.Replace(input, @"([a-z0-9])([A-Z])", "$1_$2").ToLowerInvariant();
    }
  }
}
