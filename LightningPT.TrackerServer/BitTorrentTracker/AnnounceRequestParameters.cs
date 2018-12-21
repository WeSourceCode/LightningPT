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

        public string InfoHash { get; private set; }

        public string PeerId { get; private set; }

        public long Uploaded { get; private set; }

        public long Downloaded { get; private set; }

        public TrackerEvent Event { get; private set; }

        public long Left { get; private set; }

        public string PassKey { get; private set; }
        
        public AnnounceRequestParameters(){ }

        public AnnounceRequestParameters(GetTrackInfoInput input)
        {
            if (IPAddress.TryParse(input.Ip, out IPAddress ipAddress))
            {
                ClientAddress = new IPEndPoint(ipAddress,input.Port);
            }
        }
        
        public static implicit operator AnnounceRequestParameters(GetTrackInfoInput input)
        {
            return new AnnounceRequestParameters(input);
        }
    }
}