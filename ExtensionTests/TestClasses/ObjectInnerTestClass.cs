namespace Hylasoft.Extensions.TestClasses
{
  public class ObjectInnerTestClass
  {
    public string InnerClassString { get; private set; }

    public ObjectInnerTestClass(string innerValue)
    {
      InnerClassString = innerValue;
    }
  }
}
