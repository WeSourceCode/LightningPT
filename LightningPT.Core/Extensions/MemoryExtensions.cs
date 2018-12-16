using System.IO;
using System.Threading.Tasks;

namespace LightningPT.Core.Extensions
{
    /// <summary>
    /// <see cref="MemoryStream"/> 相关的扩展方法。
    /// </summary>
    public static class MemoryExtensions
    {
        /// <summary>
        /// 向内存流当中写入一个字节的数据。
        /// </summary>
        /// <param name="stream">待写入的内存流。</param>
        /// <param name="byte">需要学入的 1 字节的数据。</param>
        public static void AppendByte(this MemoryStream stream, byte @byte)
        {
            stream.WriteByte(@byte);
        }

        /// <summary>
        /// 向内存流当中写入一个字节数组。
        /// </summary>
        /// <param name="stream">待写入的内存流。</param>
        /// <param name="bytes">需要写入的字节数组。</param>
        public static void AppendBytes(this MemoryStream stream, byte[] bytes)
        {
            stream.Write(bytes,0,bytes.Length);
        }

        /// <summary>
        /// 向内存流当中异步地写入一个字节数组。
        /// </summary>
        /// <param name="stream">待写入的字节流。</param>
        /// <param name="bytes">需要写入的字节数组。</param>
        public static Task AppendBytesAsync(this MemoryStream stream, byte[] bytes)
        {
            return stream.WriteAsync(bytes, 0, bytes.Length);
        }
    }
}