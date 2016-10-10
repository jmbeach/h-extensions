using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Hylasoft.Extensions
{
  [TestClass]
  public class StringTests
  {
    [TestMethod]
    public void ToListStringTest()
    {
      var strings = new[] {"foo", "bar", "baz"};

      const string expectedDefault = "foo, bar, baz";
      const string expectedCustom = "foo|bar|baz";

      var defaultList = strings.ToListString();
      var customList = strings.ToListString("|");

      Assert.AreEqual(expectedDefault, defaultList);
      Assert.AreEqual(expectedCustom, customList);
    }
  }
}
