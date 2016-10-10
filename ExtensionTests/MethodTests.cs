using System;
using System.Diagnostics;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Hylasoft.Extensions
{
  [TestClass]
  public class MethodTests
  {
    [TestMethod]
    public void TestMethodNames()
    {
      var stack = new StackTrace();
      var metaMethod = ((Action)TestMethodNames).Method;
      var currentMethod = stack.GetFrame(0).GetMethod();
      var currentMethodName = currentMethod.Name;
      var currentType = GetType();
      var currentSimpleName = string.Format("{0}.{1}", currentType.Name, currentMethodName);
      var currentFullName = string.Format("{0}.{1}", currentType.Namespace, currentSimpleName);

      var methodName = metaMethod.MethodName();
      var fullMethodName = metaMethod.FullMethodName();

      Assert.AreEqual(currentSimpleName, methodName);
      Assert.AreEqual(currentFullName, fullMethodName);
    }
  }
}
