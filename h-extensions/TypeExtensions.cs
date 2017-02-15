using System;

namespace Hylasoft.Extensions
{
  public static class TypeExtensions
  {
    /// <summary>
    /// Returns the default value of the type.
    /// </summary>
    /// <param name="type">The type to retrieve a default value for.</param>
    /// <returns>A boxed value of a default instance of this type.</returns>
    public static object DefaultValue(this Type type)
    {
      return type.IsValueType
        ? Activator.CreateInstance(type)
        : null;
    }

    /// <summary>
    /// Returns default value of a type.
    /// </summary>
    /// <typeparam name="TVal">The type to retrieve a default value from.</typeparam>
    /// <returns>The default value of the provided type.</returns>
    public static TVal DefaultValue<TVal>()
    {
      return (TVal) DefaultValue(typeof (TVal));
    }
  }
}
