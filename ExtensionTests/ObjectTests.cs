using System;
using Hylasoft.Extensions.TestClasses;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Hylasoft.Extensions
{
  [TestClass]
  public class ObjectTests
  {
    [TestMethod]
    public void DetailedObjectStringValueTests()
    {
      const string numValString = "3";
      const string strValString = "foo";

      var numVal = int.Parse(numValString);

      var numDetail = numVal.ToDetailedString();
      var strDetail = strValString.ToDetailedString();

      Assert.AreEqual(numDetail, numValString);
      Assert.AreEqual(strDetail, strValString);
    }

    [TestMethod]
    public void DetailedObjectStringComplexTests()
    {
      const string innerVal = "Inner Value";
      const string stringVal = "Custom String";
      const int intVal = 42;

      var stringVals = new[] {"Foo", "Bar", "Baz"};
      var innerObj = new ObjectInnerTestClass(innerVal);
      var outerObj = new ObjectTestClass(stringVal, intVal, stringVals, innerObj);

      var classDetailedString = outerObj.ToDetailedString("outerObj");
      Assert.IsTrue(!string.IsNullOrEmpty(classDetailedString));

      var lineDelimiter = new[] {Environment.NewLine};
      var detailLines = classDetailedString.Split(lineDelimiter, StringSplitOptions.RemoveEmptyEntries);

      Assert.IsNotNull(detailLines);
      Assert.AreEqual(detailLines.Length, 9);

      var fooLine = detailLines[4];
      Assert.IsTrue(fooLine.Contains("Foo"));
    }
  }
}
