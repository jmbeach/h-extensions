using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Hylasoft.Extensions
{
  public static class ObjectExtensions
  {
    public static readonly string DefaultTerminator = Environment.NewLine;
    public const string DefaultTypeWrapper = "({0}) ";
    public const string DefaultNameWrapper = "[{0}] ";
    public const string DefaultIndentation = "  ";

    /// <summary>
    /// Builds a string that represents the data in any arbitrary object.
    /// </summary>
    /// <param name="val">The object to build a detailed string of.</param>
    /// <param name="instanceName">Name given to this instance of the object.</param>
    /// <param name="entryTerminator">Line ending to use between properties. Default is an Environment.NewLine.</param>
    /// <param name="typeWrapper">Format string for property or object types. Default is "({0}) ".</param>
    /// <param name="nameWrapper">Format string for property or instance names. Default is "[{0}] ".</param>
    /// <param name="indentation">Indentation value.  Default is "  ".</param>
    /// <returns></returns>
    public static string ToDetailedString(this object val, string instanceName = null, string entryTerminator = null,
      string typeWrapper = DefaultTypeWrapper, string nameWrapper = DefaultNameWrapper, string indentation = DefaultIndentation)
    {
      return ReferenceEquals(val, null)
        ? string.Empty
        : BuildComplexDetailedString(val, instanceName, entryTerminator, typeWrapper, nameWrapper, indentation);
    }

    private static string BuildComplexDetailedString(object val, string instanceName, string entryTerminator,
      string typeWrapper, string nameWrapper, string indentation)
    {
      var type = val.GetType();
      var lines = new List<string>();
      if (entryTerminator == null)
        entryTerminator = Environment.NewLine;

      if (IsValueObject(val))
        return val.ToString();

      var complexTypes = new List<object> { val };
      lines.Add(BuildInstanceLine(instanceName, type, typeWrapper, nameWrapper, indentation));
      lines.AddRange(BuildPropertyLines(complexTypes, 1, val, type, typeWrapper, nameWrapper, indentation));

      return BuildDetailedString(entryTerminator, lines);
    }

    private static string BuildInstanceLine(string instanceName, Type instanceType,
      string typeWrapper, string nameWrapper, string indentation, int index=0)
    {
      var instanceTypeName = instanceType.Name;
      return string.IsNullOrEmpty(instanceName)
        ? instanceTypeName
        : BuildLine(index, instanceTypeName, instanceName, typeWrapper, nameWrapper, indentation, instanceType);
    }

    private static IEnumerable<string> BuildPropertyLines(List<object> complexTypes, int index, object instance,
      string typeWrapper, string nameWrapper, string indentation)
    {
      return (instance == null)
        ? new String[0]
        : IsValueObject(instance)
          ? new []{BuildLine(index, null, instance, typeWrapper, nameWrapper, indentation)}
          : BuildPropertyLines(complexTypes, index, instance, instance.GetType(), typeWrapper, nameWrapper, indentation);
    }

    private static IEnumerable<string> BuildPropertyLines(List<object> complexTypes, int index, object instance,
      IReflect instanceType, string typeWrapper, string nameWrapper, string indentation)
    {
      var properties = GetProperties(instanceType);
      return properties == null
        ? new string[0]
        : properties.SelectMany(prop => GetPropertyLines(complexTypes, index, prop, instance, typeWrapper, nameWrapper, indentation));
    }

    private static IEnumerable<PropertyInfo> GetProperties(IReflect instanceType)
    {
      return instanceType.GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.GetProperty);
    }

    private delegate IEnumerable<string> PropertyRetrieval(List<object> complexTypes, string propertyName, int index, object val,
      string typeWrapper, string nameWrapper, string indentation);

    private static IEnumerable<string> GetPropertyLines(List<object> complexTypes, int index, PropertyInfo propInfo, object instance,
      string typeWrapper, string nameWrapper, string indentation)
    {
      object property;
      if (propInfo == null || ReferenceEquals(instance, null) || ReferenceEquals(property = propInfo.GetValue(instance, null), null))
        return new string[0];

      var propName = propInfo.Name;
      var retrieval = IsValueObject(property)
        ? GetValuePropertyLine
        : IsEnumerableObject(property)
          ? (PropertyRetrieval) GetEnumerablePropertyLine
          : GetComplexPropertyLine;

      return retrieval(complexTypes, propName, index, property, typeWrapper, nameWrapper, indentation);
    }

    private static IEnumerable<string> GetValuePropertyLine(List<object> complexTypes, string propertyName, int index, object val,
      string typeWrapper, string nameWrapper, string indentation)
    {
      return new[] {BuildLine(index, propertyName, val, typeWrapper, nameWrapper, indentation)};
    }

    private static IEnumerable<string> GetEnumerablePropertyLine(List<object> complexTypes, string propertyName, int index,
      object enumerable, string typeWrapper, string nameWrapper, string indentation)
    {
      var enums = enumerable as IEnumerable;
      if (enums == null) return new string[0];

      var type = enums.GetType();
      var lines = new List<string>
      {
        BuildInstanceLine(propertyName, type, typeWrapper, nameWrapper, indentation, index)
      };

      foreach (var obj in enums)
        lines.AddRange(BuildPropertyLines(complexTypes, index+1, obj, typeWrapper, nameWrapper, indentation));

      return lines;
    }

    private static IEnumerable<string> GetComplexPropertyLine(List<object> complexTypes, string propertyName, int index, object complex,
      string typeWrapper, string nameWrapper, string indentation)
    {
      if (ReferenceEquals(complex, null) || complexTypes.Contains(complex))
        return new String[0];

      complexTypes.Add(complex);
      var type = complex.GetType();
      var lines = new List<string>
      {
        BuildInstanceLine(propertyName, type, typeWrapper, nameWrapper, indentation, index)
      };

      lines.AddRange(BuildPropertyLines(complexTypes, index+1, complex, typeWrapper, nameWrapper, indentation));
      return lines;
    }

    private static bool IsValueObject(object instance)
    {
      return instance is ValueType || instance is string;
    }

    private static bool IsEnumerableObject(object instance)
    {
      return instance is IEnumerable;
    }
    
    private static string BuildDetailedString(string terminator, IEnumerable<string> lines)
    {
      if (lines == null)
        return string.Empty;

      var lineBuilder = lines
        .Where(line => !string.IsNullOrEmpty(line))
        .Aggregate(new StringBuilder(), (builder, line) => builder.Append(line).Append(terminator));

      return lineBuilder.ToString();
    }

    private static string BuildLine(int indent, string objectName, object value,
      string typeWrapperFormat, string nameWrapperFormat, string indentation, Type valueType = null)
    {
      if (ReferenceEquals(value, null))
        return string.Empty;

      var builder = new StringBuilder();
      if (valueType == null) valueType = value.GetType();
      
      if(!string.IsNullOrEmpty(indentation))
        for (var i = 0; i < indent; i++) builder.Append(indentation);

      var typeName = valueType.Name;
      var typeSpec = string.IsNullOrEmpty(typeWrapperFormat)
        ? typeName
        : string.Format(typeWrapperFormat, typeName);

      var objectSpec = string.IsNullOrEmpty(objectName)
        ? string.Empty
        : string.Format(nameWrapperFormat, objectName);

      builder.Append(string.Format("{0}{1}: {2}", typeSpec, objectSpec, value));
      return builder.ToString();
    }
  }
}
