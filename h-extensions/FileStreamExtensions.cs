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
    /// Transforms (encrypts/decrypts) a file stream and returns an array
    /// of the transformed bytes
    /// </summary>
    /// <param name="stream">File stream to decrypt</stream>
    /// <param name="transformer">Decryption technique used to decrypt file stream</stream>
    public static byte[] Transform(this FileStream stream, ICryptoTransform transformer)
    {
      var originalBytes = stream.ToByteArray();
      byte[] transformedBytes;
      using (var transformedStream = new MemoryStream())
      {
        using (var cryptoStream = new CryptoStream(transformedStream, transformer, CryptoStreamMode.Write))
        {
          cryptoStream.Write(originalBytes, 0, originalBytes.Length);
          cryptoStream.FlushFinalBlock();
          transformedBytes = transformedStream.ToArray();
        }
      }

      return transformedBytes;
    }
  }
}
