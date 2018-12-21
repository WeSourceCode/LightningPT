using System;
using System.Net;
using BencodeNET.Objects;

namespace LightningPT.TrackerServer.BitTorrentTracker
{
    public class Peer
    {
        /// <summary>
        /// 客户端 IP 端点信息。
        /// </summary>
        public IPEndPoint ClientAddress { get; private set; }

        /// <summary>
        /// 客户端的随机 Id，由 BT 客户端生成。
        /// </summary>
        public string PeerId { get; private set; }

        /// <summary>
        /// 客户端唯一标识。
        /// </summary>
        public string UniqueId { get; private set; }

        /// <summary>
        /// 客户端在本次会话过程中下载的数据量。(以 Byte 为单位)
        /// </summary>
        public long DownLoaded { get; private set; }

        /// <summary>
        /// 客户端在本次会话过程当中上传的数据量。(以 Byte 为单位)
        /// </summary>
        public long Uploaded { get; private set; }

        /// <summary>
        /// 客户端的下载速度。(以 Byte/秒 为单位)
        /// </summary>
        public long DownloadSpeed { get; private set; }

        /// <summary>
        /// 客户端的上传速度。(以 Byte/秒 为单位)
        /// </summary>
        public long UploadSpeed { get; private set; }

        /// <summary>
        /// 客户端是否完成了当前种子，True 为已经完成，False 为还未完成。
        /// </summary>
        public bool IsCompleted { get; private set; }

        /// <summary>
        /// 最后一次请求 Tracker 服务器的时间。
        /// </summary>
        public DateTime LastRequestTrackerTime { get; private set; }

        /// <summary>
        /// 更新 Peer 的状态信息。
        /// </summary>
        /// <param name="inputParamters">客户端请求的实时参数。</param>
        public void UpdateStatus(AnnounceRequestParameters inputParamters)
        {
            var now = DateTime.Now;
            
            // 估算上传速度与下载速度
            var elapsedTime = (now - LastRequestTrackerTime).TotalSeconds;
            if (elapsedTime < 1) elapsedTime = 1;

            ClientAddress = inputParamters.ClientAddress;
        }

        /// <summary>
        /// 将 Peer 信息进行 B 编码处理为字典。
        /// </summary>
        /// <returns></returns>
        public BDictionary ToBEncodedDictionary()
        {
            return new BDictionary
            {
                {LightningPTTrackerServerConsts.PeerIdKey,new BString(PeerId)},
                {LightningPTTrackerServerConsts.Ip,new BString(ClientAddress.Address.ToString())},
                {LightningPTTrackerServerConsts.Port,new BNumber(ClientAddress.Port)}
            };
        }
    }
}