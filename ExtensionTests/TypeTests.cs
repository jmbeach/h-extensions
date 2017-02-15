using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Hylasoft.Extensions
{
  [TestClass]
  public class TypeTests
  {
    protected class TypeTestClass
    {
      public int DefaultInt { get; set; }

      public string DefaultStr { get; set; }

      public TypeTestClass DefaultClass { get; set; }
    }

    [TestMethod]
    public void DefaultTypeTest()
    {
      var testClass = new TypeTestClass();
      var defaultInt = TypeExtensions.DefaultValue<int>();
      var defaultString = TypeExtensions.DefaultValue<string>();
      var defaultClass = TypeExtensions.DefaultValue<TypeTestClass>();

      Assert.AreEqual(testClass.DefaultInt, defaultInt);
      Assert.AreEqual(defaultString, testClass.DefaultStr);
      Assert.AreEqual(defaultClass, testClass.DefaultClass);

      Assert.AreEqual(typeof(int).DefaultValue(), testClass.DefaultInt);
      Assert.AreEqual(typeof(string).DefaultValue(), testClass.DefaultStr);
      Assert.AreEqual(typeof(TypeTestClass).DefaultValue(), testClass.DefaultClass);
    }
  }
}
