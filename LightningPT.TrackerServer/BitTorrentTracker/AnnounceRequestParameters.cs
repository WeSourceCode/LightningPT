using System.Net;
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
        /// 用户唯一密钥。
        /// </summary>
        public string PassKey { get; private set; }
        
        public AnnounceRequestParameters(){ }

        public AnnounceRequestParameters(GetTrackInfoInput input)
        {
            ClientAddress = ClientAddressConvert(input);
            PeerId = input.PeerId;
            Uploaded = input.Uploaded;
            Downloaded = input.Downloaded;
            Event = EventConvert(input);
            Left = input.Left;
            PassKey = input.PassKey;
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
    }
}