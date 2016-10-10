using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Hylasoft.Extensions
{
  [TestClass]
  public class ExceptionTests
  {
    [TestMethod]
    public void TestInnerMostException()
    {
      const string firstMessage = "An argument for argument's sake.";
      const string secondMessage = "There was a critical error.";

      var first = new ArgumentException(firstMessage);
      var second = new NotSupportedException(secondMessage, first);

      Assert.IsTrue(ReferenceEquals(first, first.InnerMost()));
      Assert.IsTrue(ReferenceEquals(first, second.InnerMost()));
      Assert.IsFalse(ReferenceEquals(second, second.InnerMost()));
    }
  }
}
