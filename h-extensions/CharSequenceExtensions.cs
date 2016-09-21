using System.Collections.Generic;
using System.Linq;

namespace Hylasoft.Extensions
{
  public static class CharSequenceExtensions
  {
    /// <summary>
    /// Builds a string from a collection of char values.
    /// </summary>
    public static string BuildString(this IEnumerable<char> values)
    {
      return values == null
        ? string.Empty
        : new string(values.ToArray());
    }

    /// <summary>
    /// Returns a single, delimited list string, from an collection of char values.
    /// </summary>
    /// <param name="values">The set of strings to create a list out of.</param>
    /// <param name="separator">The separator token.  Default is a comma.</param>
    /// <returns></returns>
    public static string ToRangeString(this IEnumerable<char> values, string separator = ", ")
    {
      return values == null
        ? string.Empty
        : string.Join(separator, values.Select(chr => new string(new[] {chr})).ToArray());
    }
  }
}
