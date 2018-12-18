using System;
using Abp;

namespace LightningPT.TrackerServer.BitTorrentUtils
{
    /// <summary>
    /// B 编码异常，当调用 <see cref="BEncoding"/> 相关的序列化/反序列化时出现错误时触
    /// 发。
    /// </summary>
    [Obsolete("已经弃用，请使用 BencodeNET 库所提供的方法。")]
    public class BEncodingException : AbpException
    {
        public BEncodingException(string message) : base(message)
        {
            
        }
    }
}