using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using Abp.Dependency;

namespace LightningPT.TrackerServer.BitTorrentTracker
{
    public class BitTorrentManager : IBitTorrentManager,ISingletonDependency
    {
        private readonly ConcurrentDictionary<string, List<Peer>> _peers;

        public BitTorrentManager()
        {
            _peers = new ConcurrentDictionary<string, List<Peer>>();
        }

        public void AddPeer(string infoHash, Peer peer)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 获得可以用于完成 Announce 请求的客户端列表。
        /// </summary>
        /// <param name="infoHash">要检测的种子唯一 Id。</param>
        /// <returns>可用的客户端列表。</returns>
        public IReadOnlyList<Peer> GetPeers(string infoHash)
        {
            throw new NotImplementedException();
        }

        public void DeletePeer(string infoHash, Peer peer)
        {
            throw new NotImplementedException();
        }

        public void ClearZombiePeers(string infoHash, TimeSpan expiry)
        {
            throw new NotImplementedException();
        }

        public void UpdateInfo(string infoHash, AnnounceRequestParameters @params)
        {
            throw new NotImplementedException();
        }
    }
}