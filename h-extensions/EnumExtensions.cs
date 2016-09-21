using System;
using System.ComponentModel;
using System.Linq;

namespace Hylasoft.Extensions
{
  public static class EnumExtensions
  {
    /// <summary>
    /// Parses an enumeration from a string value.
    /// </summary>
    /// <typeparam name="TEnum">The type of enumeration to create.</typeparam>
    /// <param name="enumCode">The value to parse.</param>
    /// <returns>An enumeration of the specified value, or of default value if parsing failed.</returns>
    public static TEnum ToEnum<TEnum>(this string enumCode)
    {
      try
      {
        return (TEnum)Enum.Parse(typeof(TEnum), enumCode, true);
      }
      catch (Exception)
      {
        return (TEnum)Activator.CreateInstance(typeof(TEnum));
      }
    }

    /// <summary>
    /// Retrieves the description attribute of an enumeration value.
    /// </summary>
    public static string GetDescription<TEnum>(this TEnum enumeration)
      where TEnum : struct
    {
      var enumType = enumeration.GetType();
      var enumInfo = enumType.GetField(enumeration.ToString());
      var descriptionAttribute = enumInfo.GetCustomAttributes(typeof(DescriptionAttribute), true)
        .OfType<DescriptionAttribute>()
        .FirstOrDefault();

      return descriptionAttribute != null
        ? descriptionAttribute.Description
        : string.Empty;
    }
  }
}
