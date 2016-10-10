using System;
using System.Reflection;

namespace Hylasoft.Extensions
{
  public static class MethodExtensions
  {
    /// <summary>
    /// Returns the name of the object and method, in the format of: Object.Method.
    /// </summary>
    public static string MethodName(this MethodInfo method)
    {
      return MethodName(method, false);
    }

    /// <summary>
    /// Returns the name of the object, method, and full namespace of the object.
    /// </summary>
    /// <param name="method"></param>
    /// <returns></returns>
    public static string FullMethodName(this MethodInfo method)
    {
      return MethodName(method, true);
    }

    private static string MethodName(MethodInfo method, bool fullNamespace)
    {
      if (method == null)
        return string.Empty;

      Type declared;
      if ((declared = method.DeclaringType) == null)
        return method.Name;

      var methodName = method.Name;
      var className = fullNamespace
        ? declared.FullName
        : declared.Name;

      return string.Format("{0}.{1}", className, methodName); 
    }
  }
}
