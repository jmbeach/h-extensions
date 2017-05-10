using Hylasoft.Extensions.TestClasses;
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
    public void ToEnumFromIntTest()
    {
      const int foo = 1;
      const int bar = 2;
      const int invalid = 5;

      var fooEnum = foo.ToEnum<TestEnum>();
      var barEnum = bar.ToEnum<TestEnum>();
      var invalidEnum = invalid.ToEnum<TestEnum>();

      Assert.AreEqual(fooEnum, TestEnum.Foo);
      Assert.AreEqual(barEnum, TestEnum.Bar);
      Assert.AreEqual(invalidEnum, TestEnum.NotAValue);
    }

    [TestMethod]
    public void ToIntFromEnumTest()
    {
      const int foo = 1;
      const int bar = 2;
      const int invalid = 0;

      Assert.AreEqual(foo, TestEnum.Foo.ToInt());
      Assert.AreEqual(bar, TestEnum.Bar.ToInt());
      Assert.AreEqual(invalid, TestEnum.NotAValue.ToInt());
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

    [TestMethod]
    public void EnumParseTest()
    {
      const string secondEnumName = "SecondValue";
      const string secondEnumDescription = "Second Value";
      const string badEnumName = "NotAValue";

      const ComplexTestEnum defaultEnum = ComplexTestEnum.Uninitialized;
      const ComplexTestEnum secondEnum = ComplexTestEnum.SecondValue;

      var parsedName = secondEnumName.ToEnum<ComplexTestEnum>();
      Assert.AreEqual(parsedName, secondEnum);

      var parsedDescription = secondEnumDescription.ToEnum<ComplexTestEnum>();
      Assert.AreEqual(parsedDescription, secondEnum);

      var unparsedEnum = badEnumName.ToEnum<ComplexTestEnum>();
      Assert.AreEqual(unparsedEnum, defaultEnum);
    }
  }
}
