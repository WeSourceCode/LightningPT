using LightningPT.TrackerServer.AnnounceService.Dtos.InputDto;

namespace LightningPT.TrackerServer.BitTorrentTracker
{
    public class AnnounceRequestParameters
    {
        public static implicit operator GetTrackInfoInput(AnnounceRequestParameters input)
        {
            return null;
        }
        
        public static implicit operator AnnounceRequestParameters(GetTrackInfoInput input)
        {
            return null;
        }
    }
}