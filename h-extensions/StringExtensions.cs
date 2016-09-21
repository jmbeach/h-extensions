using System.Collections.Generic;
using System.Linq;

namespace Hylasoft.Extensions
{
  public static class StringExtensions
  {
    /// <summary>
    /// Returns a single, delimited list string, from an collection of string values.
    /// </summary>
    /// <param name="values">The set of strings to create a list out of.</param>
    /// <param name="separator">The separator token.  Default is a comma.</param>
    public static string ToListString(this IEnumerable<string> values, string separator = ", ")
    {
      return values == null 
        ? string.Empty 
        : string.Join(separator, (values.ToArray()));
    }
  }
}
