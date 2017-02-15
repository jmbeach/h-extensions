using System;
using System.Collections.ObjectModel;

namespace Hylasoft.Extensions.TestClasses
{
  public class ObjectTestClass
  {
    public string TestString { get; private set; }

    public int TestInt { get; private set; }

    public Collection<string> TestStrings { get; private set; }

    public ObjectInnerTestClass InnerClass { get; private set; }

    public Type TestType { get; private set; }

    public ObjectTestClass(string testString, int testInt, string[] testStrings, ObjectInnerTestClass inner, Type testType)
    {
      TestType = testType;
      TestString = testString;
      TestInt = testInt;
      TestStrings = new Collection<string>(testStrings);
      InnerClass = inner;
    }
  }
}
