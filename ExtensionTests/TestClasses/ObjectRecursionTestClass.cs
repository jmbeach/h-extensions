namespace Hylasoft.Extensions.TestClasses
{
  public class ObjectRecursionTestClass
  {
    public string TestStr { get; private set; }

    public ObjectInnerTestClass Inner { get; private set; }

    public ObjectRecursionTestClass Self { get; private set; }

    public ObjectRecursionTestClass(string testStr, ObjectInnerTestClass inner)
    {
      Inner = inner;
      TestStr = testStr;
      Self = this;
    }
  }
}
