using BencodeNET.Objects;

namespace LightningPT.TrackerServer.BitTorrentTracker
{
    public class BitTorrentStatus
    {
        public BNumber Complete { get; set; }

        public BNumber InComplete { get; set; }

        public BNumber Downloaded { get; set; }

        public BitTorrentStatus()
        {
            Complete = new BNumber(0);
            InComplete = new BNumber(0);
            Downloaded = new BNumber(0);
        }
    }
}