using System;
using System.IO;

namespace Hylasoft.Extensions
{
  public static class FileStreamExtensions
  {
    /// <summary>
    /// Converts the file stream into an array of bytes.
    /// <param name="stream">Stream to convert into byte array</param>
    /// </summary>
    public static byte[] ToByteArray(this FileStream stream)
    {
      var bytes = new byte[stream.Length];
      stream.Read(bytes, 0, bytes.Length);
      return bytes;
    }
  }
}
