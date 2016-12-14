using System.Collections.Generic;
using System.Collections.ObjectModel;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Hylasoft.Extensions
{
  [TestClass]
  public class EnumerationTests
  {
    [TestMethod]
    public void EnumerationToCollectionTest()
    {
      var testSet = new[]
      {
        1, 2, 3, 4
      };

      var testCollection = testSet.ToCollection();
      Assert.AreEqual(testCollection.GetType(), typeof(Collection<int>));
      AssertComparable(testCollection, testSet);
    }

    [TestMethod]
    public void EnumerationForEachTest()
    {
      var valuesToSet = new List<int>();
      var initialValues = new[]
      {
        1, 2, 3, 4
      };

      initialValues.ForEach(valuesToSet.Add);
      AssertComparable(valuesToSet, initialValues);
    }

    protected void AssertComparable<TVal>(IList<TVal> a, IList<TVal> b)
    {
      Assert.AreEqual(a.Count, a.Count);

      for (var i = 0; i < a.Count; i++)
        Assert.AreEqual(a[i], b[i]);
    }
  }
}
