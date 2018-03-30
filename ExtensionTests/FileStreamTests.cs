using System;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Hylasoft.Extensions
{
  [TestClass]
  public class FileStreamTests
  {
    [TestMethod]
    public void FileStreamToByteArrayTest()
    {
      using (var stream = new FileStream("..\\..\\byte-test-file.txt", FileMode.Open))
      {
        var bytes = stream.ToByteArray();
        Assert.AreEqual(816, bytes.Length);
      }
    }
  }
}
