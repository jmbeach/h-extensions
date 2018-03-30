using System.IO;
using System.Security.Cryptography;
using Hylasoft.ExtensionTests.Constants;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Text;
using System.Linq;

namespace Hylasoft.Extensions
{
  [TestClass]
  public class FileStreamTests
  {
    [TestMethod]
    public void FileStreamToByteArrayTest()
    {
      using (var stream = new FileStream(TestResources.FilePaths.ByteTestFile, FileMode.Open))
      {
        var bytes = stream.ToByteArray();
        Assert.AreEqual(816, bytes.Length);
      }
    }

    [TestMethod]
    public void FileStreamEncryptDecryptTest()
    {
      using (var decryptedStream = new FileStream(TestResources.FilePaths.ByteTestFile, FileMode.Open))
      {
        var bytes = decryptedStream.ToByteArray();
        using (var encryptedStream = new FileStream(TestResources.FilePaths.ByteTestFileEncrypted, FileMode.Open))
        {
          var key = Encoding.Unicode.GetBytes("12345678");
          var iv = Encoding.Unicode.GetBytes("12345678");
          var transform = new RijndaelManaged();
          var decrypter = transform.CreateDecryptor(key, iv);
          var decrypted = encryptedStream.Transform(decrypter);
          Assert.IsTrue(Enumerable.SequenceEqual(bytes, decrypted));
        }
      }
    }
  }
}
