using System.ComponentModel;

namespace Hylasoft.Extensions.TestClasses
{
  public enum ComplexTestEnum
  {
    [Description("Uknown")]
    Uninitialized = 0x0,

    [Description("First Value")]
    FirstValue = 0x1,

    [Description("Second Value")]
    SecondValue = 0x2,

    [Description("Third Value")]
    ThirdValue = 0x3
  }
}
