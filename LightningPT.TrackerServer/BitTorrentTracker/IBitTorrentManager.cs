using System;
using System.Collections.Generic;

namespace LightningPT.TrackerServer.BitTorrentTracker
{
    public interface IBitTorrentManager
    {
        void AddPeer(Peer peer);

        IReadOnlyList<Peer> GetPeers();

        void DeletePeer(Peer peer);

        void ClearZombiePeers(TimeSpan expiry);
    }
}