using System;
using System.Collections.Generic;

namespace LightningPT.TrackerServer.BitTorrentTracker
{
    public interface IBitTorrentManager
    {
        void AddPeer(string infoHash,Peer peer);

        IReadOnlyList<Peer> GetPeers(string infoHash);

        void DeletePeer(string infoHash,Peer peer);

        void ClearZombiePeers(string infoHash, TimeSpan expiry);

        void UpdateInfo(string infoHash,AnnounceRequestParameters @params);
    }
}