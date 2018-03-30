using System.IO;
using System.Security.Cryptography;

namespace Hylasoft.Extensions
{
  public static class FileStreamExtensions
  {
    /// <summary>
    /// Converts the file stream into an array of bytes.
    /// </summary>
    /// <param name="stream">Stream to convert into byte array</param>
    public static byte[] ToByteArray(this FileStream stream)
    {
      var bytes = new byte[stream.Length];
      stream.Read(bytes, 0, bytes.Length);
      return bytes;
    }

    /// <summary>
    /// Decrypts a file stream and returns an array of the decrypted bytes
    /// </summary>
    /// <param name="stream">File stream to decrypt</stream>
    /// <param name="algorithm">Decryption technique used to decrypt file stream</stream>
    public static byte[] Decrypt(this FileStream stream, ICryptoTransform algorithm)
    {
      var encryptedBytes = stream.ToByteArray();
      var decryptedStream = new MemoryStream();
      var cryptoStream = new CryptoStream(decryptedStream, algorithm, CryptoStreamMode.Write);
      cryptoStream.FlushFinalBlock();
      decryptedStream.Position = 0;
      var decryptedBytes = new byte[encryptedBytes.Length];
      decryptedStream.Read(decryptedBytes, 0, (int)decryptedStream.Length);
      cryptoStream.Close();
      decryptedStream.Close();
      return decryptedBytes;
    }
  }
}
