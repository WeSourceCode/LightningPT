using LightningPT.TrackerServer.AnnounceService.Dtos.InputDto;
using LightningPT.TrackerServer.BitTorrentTracker;

namespace LightningPT.TrackerServer.AnnounceService
{
    public class AnnounceAppService : IAnnounceAppService
    {
        private readonly ITracker _tracker;

        public AnnounceAppService(ITracker tracker)
        {
            _tracker = tracker;
        }

        public void GetTrackInfo(GetTrackInfoInput input)
        {
            _tracker.HandleRequest(input);
        }
    }
}