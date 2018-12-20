namespace LightningPT.TrackerServer.BitTorrentTracker
{
    public interface ITracker
    {
        void HandleRequest(AnnounceRequestParameters request);
    }
}