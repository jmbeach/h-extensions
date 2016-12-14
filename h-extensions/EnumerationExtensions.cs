using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Hylasoft.Extensions
{
  public static class EnumerationExtensions
  {
    /// <summary>
    /// Translates any enumeration into a collection of the same type.
    /// </summary>
    /// <typeparam name="TVal">The type of object for the collection.</typeparam>
    /// <param name="valueSet">The IEnumerable values.</param>
    /// <returns>Returns a Collection<TVal> of valueSet.</TVal></returns>
    public static Collection<TVal> ToCollection<TVal>(this IEnumerable<TVal> valueSet)
    {
      var valueArray = valueSet == null
        ? new TVal[0]
        : valueSet.ToArray();

      return new Collection<TVal>(valueArray);
    }

    /// <summary>
    /// Performs an action on each value of a set.
    /// </summary>
    /// <typeparam name="TVal">The type associated with the set.</typeparam>
    /// <param name="valueSet">The set to perform actions on.</param>
    /// <param name="action">The action to perform.</param>
    public static void ForEach<TVal>(this IEnumerable<TVal> valueSet, Action<TVal> action)
    {
      foreach (var value in valueSet)
        action(value);
    }
  }
}
