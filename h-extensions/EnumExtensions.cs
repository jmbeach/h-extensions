using System;
using System.ComponentModel;
using System.Globalization;
using System.Linq;

namespace Hylasoft.Extensions
{
  public static class EnumExtensions
  {
    /// <summary>
    /// Parses an enumeration from a string value.  First attempts to parse by name.  If that fails, it attempts to parse by DescriptionAttribute.  If that fails, it returns the default instance.
    /// </summary>
    /// <typeparam name="TEnum">The type of enumeration to create.</typeparam>
    /// <param name="enumCode">The value to parse.</param>
    /// <returns>An enumeration of the specified value, or of default value if parsing failed.</returns>
    public static TEnum ToEnum<TEnum>(this string enumCode)
      where TEnum : struct, IConvertible
    {
      try
      {
        TEnum enumVal;
        if (Enum.TryParse(enumCode, true, out enumVal))
          return enumVal;

        return TryParseEnumFromDescription(enumCode, out enumVal)
          ? enumVal
          : BuildDefaultEnum<TEnum>();
      }
      catch (Exception)
      {
        return BuildDefaultEnum<TEnum>();
      }
    }

    /// <summary>
    /// Builds an instance of enumeration from an integer value.
    /// </summary>
    /// <typeparam name="TEnum">The type of enumeration to build.</typeparam>
    /// <param name="enumValue">The value of the enumeration.</param>
    public static TEnum ToEnum<TEnum>(this int enumValue)
      where TEnum : struct, IConvertible
    {
      return Enum.IsDefined(typeof (TEnum), enumValue)
        ? ConvertToValue<int, TEnum>(enumValue)
        : BuildDefaultEnum<TEnum>();
    }

    /// <summary>
    /// Retrieves the integer value of an enumeration.
    /// </summary>
    /// <typeparam name="TEnum">The type of enumeration.</typeparam>
    /// <param name="enumValue">The value of the enumeration.</param>
    /// <returns></returns>
    public static int ToInt<TEnum>(this TEnum enumValue)
      where TEnum : struct, IConvertible
    {
      return ConvertToValue<TEnum, int>(enumValue);
    }

    /// <summary>
    /// Retrieves the description attribute of an enumeration value.
    /// </summary>
    public static string GetDescription<TEnum>(this TEnum enumeration)
      where TEnum : struct, IConvertible
    {
      var enumType = enumeration.GetType();
      var enumInfo = enumType.GetField(enumeration.ToString(CultureInfo.InvariantCulture));
      var descriptionAttribute = enumInfo.GetCustomAttributes(typeof(DescriptionAttribute), true)
        .OfType<DescriptionAttribute>()
        .FirstOrDefault();

      return descriptionAttribute != null
        ? descriptionAttribute.Description
        : string.Empty;
    }


    private static bool TryParseEnumFromDescription<TEnum>(string description, out TEnum enumVal)
      where TEnum : struct, IConvertible
    {
      var enumType = typeof (TEnum);
      var descriptions = enumType.GetMemberAttributes<DescriptionAttribute>();

      enumVal = BuildDefaultEnum<TEnum>();
      var targetMember = descriptions.Where(pair => pair.Attributes.Any(attr => attr.Description == description))
        .Select(pair => pair.Member)
        .FirstOrDefault();

      return targetMember != null && Enum.TryParse(targetMember.Name, out enumVal);
    }

    private static TEnum BuildDefaultEnum<TEnum>()
      where TEnum : struct, IConvertible
    {
      return (TEnum) Activator.CreateInstance(typeof (TEnum));
    }

    /// <summary>
    /// Attempts to convert a value from one type to another.
    /// </summary>
    /// <typeparam name="TInput">The initial type of the value.</typeparam>
    /// <typeparam name="TVal">The intended converted type of the value.</typeparam>
    /// <param name="input">The value to be converted.</param>
    private static TVal ConvertToValue<TInput, TVal>(TInput input)
    {
      var valType = typeof(TVal);
      var defaultVal = (TVal)valType.DefaultValue();

      try
      {
        return (TVal)(object)input;
      }
      catch
      {
        return defaultVal;
      }
    }
  }
}
