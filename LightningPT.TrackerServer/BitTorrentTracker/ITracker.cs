using System.Threading.Tasks;
using BencodeNET.Objects;

namespace LightningPT.TrackerServer.BitTorrentTracker
{
    /// <summary>
    /// Tracker 服务器的基本定义。
    /// </summary>
    public interface ITracker
    {
        /// <summary>
        /// 处理客户端的 Tracker 请求。
        /// </summary>
        /// <param name="request">客户端的请求参数。</param>
        /// <returns>Tracker 处理的结果。</returns>
        Task<BDictionary> HandleRequest(AnnounceRequestParameters request);
    }
}