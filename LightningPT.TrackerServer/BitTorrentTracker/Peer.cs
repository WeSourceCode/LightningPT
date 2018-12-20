using System;
using System.Net;

namespace LightningPT.TrackerServer.BitTorrentTracker
{
    public class Peer
    {
        /// <summary>
        /// 客户端 IP 端点信息。
        /// </summary>
        public IPEndPoint ClientAddress { get; set; }

        /// <summary>
        /// 客户端的随机 Id，由 BT 客户端生成。
        /// </summary>
        public string PeerId { get; set; }

        /// <summary>
        /// 客户端唯一标识。
        /// </summary>
        public string UniqueId { get; set; }

        /// <summary>
        /// 客户端在本次会话过程中下载的数据量。(以 Byte 为单位)
        /// </summary>
        public long DownLoaded { get; set; }

        /// <summary>
        /// 客户端在本次会话过程当中上传的数据量。(以 Byte 为单位)
        /// </summary>
        public long Uploaded { get; set; }

        /// <summary>
        /// 客户端的下载速度。(以 Byte/秒 为单位)
        /// </summary>
        public long DownloadSpeed { get; set; }

        /// <summary>
        /// 客户端的上传速度。(以 Byte/秒 为单位)
        /// </summary>
        public long UploadSpeed { get; set; }

        /// <summary>
        /// 客户端是否完成了当前种子，True 为已经完成，False 为还未完成。
        /// </summary>
        public bool IsCompleted { get; set; }

        /// <summary>
        /// 最后一次请求 Tracker 服务器的时间。
        /// </summary>
        public DateTime LastRequestTrackerTime { get; set; }

        
        public void UpdateStatus()
        {
            var now = DateTime.Now;
            
            // 估算上传速度与下载速度
            var elapsedTime = (now - LastRequestTrackerTime).TotalSeconds;
            if (elapsedTime < 1) elapsedTime = 1;

            ClientAddress = null;
        }
    }
}