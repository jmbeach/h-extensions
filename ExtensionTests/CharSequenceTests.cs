using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Hylasoft.Extensions
{
  [TestClass]
  public class CharSequenceTests
  {
    [TestMethod]
    public void TestBuildCharString()
    {
      var chars = new[] {'f', 'o', 'o'};
      var charString = chars.BuildString();
      const string expected = "foo";

      Assert.AreEqual(charString, expected);
    }

    [TestMethod]
    public void TestCharToRangeString()
    {
      var chars = new[] {'f', 'o', 'o'};
      var defaultRange = chars.ToRangeString();
      var customRange = chars.ToRangeString(":");

      const string expectedDefault = "f, o, o";
      const string expectedCustom = "f:o:o";

      Assert.AreEqual(defaultRange, expectedDefault);
      Assert.AreEqual(customRange, expectedCustom);
    }
  }
}
