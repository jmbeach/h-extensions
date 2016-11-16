using System;
using System.Collections.ObjectModel;
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

    [TestMethod]
    public void DetailedObjectStringRecursionTest()
    {
      const string innerVal = "Inner Value";
      const string stringVal = "Custom String";

      var innerObj = new ObjectInnerTestClass(innerVal);
      var recursiveObj = new ObjectRecursionTestClass(stringVal, innerObj);

      var detailedString = recursiveObj.ToDetailedString();

      var delimiters = new[] {Environment.NewLine};
      var lines = detailedString.Split(delimiters, StringSplitOptions.RemoveEmptyEntries);

      Assert.IsNotNull(lines);
      Assert.AreEqual(lines.Length, 4);
    }

    [TestMethod]
    public void DetailedObjectCollectionRecursionTest()
    {
      var testCollection = new Collection<ObjectInnerTestClass>
      {
        new ObjectInnerTestClass("foo"),
        new ObjectInnerTestClass("baz")
      };

      var detailedString = testCollection.ToDetailedString();

      Assert.IsFalse(string.IsNullOrEmpty(detailedString));
      var delimiters = new[] {Environment.NewLine};
      var lines = detailedString.Split(delimiters, StringSplitOptions.RemoveEmptyEntries);

      const int lineCount = 4;
      Assert.IsNotNull(lines);
      Assert.AreEqual(lines.Length, lineCount);
    }
  }
}
