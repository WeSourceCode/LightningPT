using System;
using System.Net;
using System.Web;
using BencodeNET.Objects;
using LightningPT.TrackerServer.AnnounceService.Dtos.InputDto;

namespace LightningPT.TrackerServer.BitTorrentTracker
{
    public class AnnounceRequestParameters
    {
        /// <summary>
        /// 客户端 IP 端点信息。
        /// </summary>
        public IPEndPoint ClientAddress { get; private set; }

        /// <summary>
        /// 种子的唯一 Hash 标识。
        /// </summary>
        public string InfoHash { get; private set; }

        /// <summary>
        /// 客户端的随机 Id，由 BT 客户端生成。
        /// </summary>
        public string PeerId { get; private set; }

        /// <summary>
        /// 已经上传的数据大小。
        /// </summary>
        public long Uploaded { get; private set; }

        /// <summary>
        /// 已经下载的数据大小。
        /// </summary>
        public long Downloaded { get; private set; }

        /// <summary>
        /// 事件表示，具体可以转换为 <see cref="TorrentEvent"/> 枚举的具体值。
        /// </summary>
        public TorrentEvent Event { get; private set; }

        /// <summary>
        /// 该客户端剩余待下载的数据。
        /// </summary>
        public long Left { get; private set; }

        /// <summary>
        /// Peer 是否允许启用压缩。
        /// </summary>
        public bool IsEnableCompact { get; private set; }

        /// <summary>
        /// Peer 想要获得的可用的 Peer 数量。
        /// </summary>
        public int PeerWantCount { get; private set; }
        
        /// <summary>
        /// 用户唯一密钥。
        /// </summary>
        public string PassKey { get; private set; }

        /// <summary>
        /// 如果出现了异常，则本属性则存放有具体的错误信息。
        /// </summary>
        public BDictionary Error { get; private set; }
        
        public AnnounceRequestParameters(){ }

        public AnnounceRequestParameters(GetTrackInfoInput input)
        {
            Error = new BDictionary();
            
            ClientAddress = ClientAddressConvert(input);
            InfoHash = InfoHashConvert(input);
            PeerId = input.Peer_Id;
            Uploaded = input.Uploaded;
            Downloaded = input.Downloaded;
            Event = EventConvert(input);
            Left = input.Left;
            PassKey = input.PassKey;
            IsEnableCompact = input.Compact == 1;
            PeerWantCount = input.NumWant;
        }
        
        public static implicit operator AnnounceRequestParameters(GetTrackInfoInput input)
        {
            return new AnnounceRequestParameters(input);
        }

        private IPEndPoint ClientAddressConvert(GetTrackInfoInput input)
        {
            if (IPAddress.TryParse(input.Ip, out IPAddress ipAddress))
            {
                return new IPEndPoint(ipAddress,input.Port);
            }

            return null;
        }

        private TorrentEvent EventConvert(GetTrackInfoInput input)
        {
            switch (input.Event)
            {
                case "started":
                    return TorrentEvent.Started;
                case "stopped":
                    return TorrentEvent.Stopped;
                case "completed":
                    return TorrentEvent.Completed;
                default:
                    return TorrentEvent.None;
            }
        }

        private string InfoHashConvert(GetTrackInfoInput input)
        {
            var infoHashBytes = HttpUtility.UrlDecodeToBytes(input.Info_Hash);
            if (infoHashBytes == null)
            {
                Error.Add(LightningPTTrackerServerConsts.FailureKey,new BString($"InfoHash 不能为空。"));
                return string.Empty;
            }
            
            if (infoHashBytes.Length != 20)
            {
                Error.Add(LightningPTTrackerServerConsts.FailureKey,new BString($"InfoHash 字段的长度 （{infoHashBytes.Length}） 不符合 BT 协议规范。"));
            }

            return BitConverter.ToString(infoHashBytes);
        }
    }
}