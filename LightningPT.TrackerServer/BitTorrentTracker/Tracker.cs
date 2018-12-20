using System.Collections.Generic;
using Abp.Dependency;

namespace LightningPT.TrackerServer.BitTorrentTracker
{
    public class Tracker : ITracker,ISingletonDependency
    {
        private IBitTorrentManager _bitTorrentManager;

        public Tracker(IBitTorrentManager bitTorrentManager)
        {
            _bitTorrentManager = bitTorrentManager;
        }

        public void HandleRequest(AnnounceRequestParameters request)
        {
            throw new System.NotImplementedException();
        }
    }
}