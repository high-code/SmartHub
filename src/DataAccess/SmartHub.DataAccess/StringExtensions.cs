using System.Text.RegularExpressions;

namespace SmartHub.DataAccess
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
