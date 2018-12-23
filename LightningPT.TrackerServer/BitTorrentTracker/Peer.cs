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
        /// Peer 还需要下载的数量。
        /// </summary>
        public long Left { get; private set; }
        
        public Peer(AnnounceRequestParameters inputParameters)
        {
            UniqueId = inputParameters.ClientAddress.ToString();
            UpdateStatus(inputParameters);
        }
        
        /// <summary>
        /// 更新 Peer 的状态信息。
        /// </summary>
        /// <param name="inputParameters">客户端请求的实时参数。</param>
        public void UpdateStatus(AnnounceRequestParameters inputParameters)
        {
            var now = DateTime.Now;
            
            // 估算上传速度与下载速度
            var elapsedTime = (now - LastRequestTrackerTime).TotalSeconds;
            if (elapsedTime < 1) elapsedTime = 1;

            ClientAddress = inputParameters.ClientAddress;
            DownloadSpeed = (int) ((inputParameters.Downloaded - DownLoaded) / elapsedTime);
            UploadSpeed = (int) ((inputParameters.Uploaded - Uploaded) / elapsedTime);
            DownLoaded = inputParameters.Downloaded;
            Uploaded = inputParameters.Uploaded;
            Left = inputParameters.Left;
            PeerId = inputParameters.PeerId;
            LastRequestTrackerTime = now;
            if (Left == 0) IsCompleted = true;
        }

        /// <summary>
        /// 将 Peer 信息进行 B 编码处理为字典。
        /// </summary>
        public BDictionary ToBEncodedDictionary()
        {
            return new BDictionary
            {
                {LightningPTTrackerServerConsts.PeerIdKey,new BString(PeerId)},
                {LightningPTTrackerServerConsts.Ip,new BString(ClientAddress.Address.ToString())},
                {LightningPTTrackerServerConsts.Port,new BNumber(ClientAddress.Port)}
            };
        }

        /// <summary>
        /// 将 Peer 信息编码为字节集数据。
        /// </summary>
        public byte[] ToBytes()
        {
            var portBytes = BitConverter.GetBytes(IPAddress.HostToNetworkOrder((short)ClientAddress.Port));
            var addressBytes = ClientAddress.Address.GetAddressBytes();

            var resultBytes = new byte[addressBytes.Length + portBytes.Length];
            
            // 首部 4 字节为 IP 地址，尾部 2 字节为端口地址。
            Array.Copy(addressBytes,resultBytes,addressBytes.Length);
            Array.Copy(portBytes,0,resultBytes,addressBytes.Length,portBytes.Length);

            return resultBytes;
        }
    }
}