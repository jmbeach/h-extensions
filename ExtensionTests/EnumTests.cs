using Microsoft.VisualStudio.TestTools.UnitTesting;
using EnumDescription  = System.ComponentModel.DescriptionAttribute;

namespace Hylasoft.Extensions
{
  internal static class TestEnumValues
  {
    public const string NotAValueDescription = "An invalid value.";
    public const string FooDescription = "Foo Value";
  }

  internal enum TestEnum
  {
    [EnumDescription(TestEnumValues.NotAValueDescription)]
    NotAValue = 0x0,

    [EnumDescription(TestEnumValues.FooDescription)]
    Foo = 0x1,
    Bar = 0x2
  }

  [TestClass]
  public class EnumTests
  {
    [TestMethod]
    public void ToEnumTest()
    {
      const string foo = "Foo";
      const string invalid = "Baz";

      var fooEnum = foo.ToEnum<TestEnum>();
      var invalidEnum = invalid.ToEnum<TestEnum>();

      Assert.AreEqual(fooEnum, TestEnum.Foo);
      Assert.AreEqual(invalidEnum, TestEnum.NotAValue);
    }

    [TestMethod]
    public void GetDescriptionTest()
    {
      var invalidDesc = TestEnum.NotAValue.GetDescription();
      var fooDesc = TestEnum.Foo.GetDescription();
      var barDesc = TestEnum.Bar.GetDescription();

      Assert.AreEqual(invalidDesc, TestEnumValues.NotAValueDescription);
      Assert.AreEqual(fooDesc, TestEnumValues.FooDescription);
      Assert.IsTrue(string.IsNullOrEmpty(barDesc));
    }


  }
}
