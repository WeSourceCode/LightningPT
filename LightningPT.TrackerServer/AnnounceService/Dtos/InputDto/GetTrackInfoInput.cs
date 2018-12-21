using LightningPT.TrackerServer.BitTorrentTracker;

namespace LightningPT.TrackerServer.AnnounceService.Dtos.InputDto
{
    public class GetTrackInfoInput
    {
        /// <summary>
        /// 种子的唯一 Hash 标识。
        /// </summary>
        public string Info_Hash { get; set; }

        /// <summary>
        /// 客户端的随机 Id，由 BT 客户端生成。
        /// </summary>
        public string PeerId { get; set; }

        /// <summary>
        /// 客户端的 IP 地址。
        /// </summary>
        public string Ip { get; set; }

        /// <summary>
        /// 客户端监听的端口。
        /// </summary>
        public int Port { get; set; }

        /// <summary>
        /// 已经上传的数据大小。
        /// </summary>
        public long Uploaded { get; set; }

        /// <summary>
        /// 已经下载的数据大小。
        /// </summary>
        public long Downloaded { get; set; }

        /// <summary>
        /// 事件表示，具体可以转换为 <see cref="TrackerEvent"/> 枚举的具体值。
        /// </summary>
        public string Event { get; set; }

        /// <summary>
        /// 该客户端剩余待下载的数据。
        /// </summary>
        public long Left { get; set; }

        /// <summary>
        /// 用户唯一密钥。
        /// </summary>
        public string PassKey { get; set; }
    }
}