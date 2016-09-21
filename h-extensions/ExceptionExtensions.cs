using System;

namespace Hylasoft.Extensions
{
  public static class ExceptionExtensions
  {
    /// <summary>
    /// Returns the inner most exception.
    /// </summary>
    public static Exception InnerMost(this Exception e)
    {
      for (; e != null && e.InnerException != null; e = e.InnerException){}
      return e;
    }
  }
}
