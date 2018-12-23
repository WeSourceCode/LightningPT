using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using Abp.Dependency;
using Abp.UI;

namespace LightningPT.TrackerServer.BitTorrentTracker
{
    public class BitTorrentManager : IBitTorrentManager,ISingletonDependency
    {
        private readonly ConcurrentDictionary<string, List<Peer>> _peers;
        private readonly ConcurrentDictionary<string, BitTorrentStatus> _peerStatuses;

        public BitTorrentManager()
        {
            _peerStatuses = new ConcurrentDictionary<string, BitTorrentStatus>();
            _peers = new ConcurrentDictionary<string, List<Peer>>();
        }

        public Peer AddPeer(string infoHash, Peer peer)
        {
            if(string.IsNullOrEmpty(infoHash)) throw new UserFriendlyException(nameof(infoHash),"种子的唯一标识对象不能为空。");
            if(peer == null) throw new UserFriendlyException(nameof(peer),"Peer 对象不能为空。");
            
            if (!_peers.ContainsKey(infoHash))
            {
                _peers.TryAdd(infoHash, new List<Peer>
                {
                    peer
                });
            }
            
            _peers[infoHash].Add(peer);

            UpdateCounts(infoHash);
            return peer;
        }

        public IReadOnlyList<Peer> GetPeers(string infoHash)
        {
            if (_peers.TryGetValue(infoHash, out List<Peer> peers))
            {
                return peers;
            }

            return null;
        }

        public void DeletePeer(string infoHash, Peer peer)
        {
            if(string.IsNullOrEmpty(infoHash)) throw new UserFriendlyException(nameof(infoHash),"种子的唯一标识对象不能为空。");
            if(peer == null) throw new UserFriendlyException(nameof(peer),"Peer 对象不能为空。");

            if (!_peers.ContainsKey(infoHash)) return;

            _peers[infoHash].Remove(peer);
            
            UpdateCounts(infoHash);
        }

        public void ClearZombiePeers(string infoHash, TimeSpan expiry)
        {
            var peers = _peers[infoHash];
            var now = DateTime.Now;

            foreach (var peer in peers)
            {
                if (now - peer.LastRequestTrackerTime < expiry) continue;

                peers.Remove(peer);
            }
        }

        public void UpdateInfo(AnnounceRequestParameters @params)
        {
            if(string.IsNullOrEmpty(@params.InfoHash)) throw new UserFriendlyException("参数不能为空。","种子的唯一标识对象不能为空。");

            if (!_peers.ContainsKey(@params.InfoHash))
            {
                _peers.TryAdd(@params.InfoHash, new List<Peer>());
            }

            if (!_peerStatuses.ContainsKey(@params.InfoHash))
            {
                _peerStatuses.TryAdd(@params.InfoHash, new BitTorrentStatus());
            }

            var peers = _peers[@params.InfoHash];
            
            var peer = peers.FirstOrDefault(x => x.UniqueId == @params.ClientAddress.ToString());
            if (peer == null)
            {
                peer = AddPeer(@params.InfoHash,new Peer(@params));
            }
            else
            {
                peer.UpdateStatus(@params);
            }

            // 如果当前 Peer 已经下载完成，则进行自增。
            if (@params.Event == TorrentEvent.Completed)
            {
                _peerStatuses[@params.InfoHash].Downloaded++;
            }else if (@params.Event == TorrentEvent.Stopped)
            {
                DeletePeer(@params.InfoHash, peer);
            }
                
            // 更新 Tracker 统计信息。
            UpdateCounts(@params.InfoHash);
        }

        public int GetComplete(string infoHash)
        {
            if (_peerStatuses.TryGetValue(infoHash, out BitTorrentStatus status))
            {
                return status.Complete;
            }

            return 0;
        }

        public int GetInComplete(string infoHash)
        {
            if (_peerStatuses.TryGetValue(infoHash, out BitTorrentStatus status))
            {
                return status.InComplete;
            }

            return 0;
        }

        /// <summary>
        /// 更新种子所对应的 Peer 状态信息。
        /// </summary>
        /// <param name="infoHash">种子的唯一 Hash Id。</param>
        private void UpdateCounts(string infoHash)
        {
            if (_peers.TryGetValue(infoHash, out List<Peer> peers))
            {
                int complete = 0, incomplete = 0; 
                foreach (var peer in peers)
                {
                    if (peer.IsCompleted)
                    {
                        complete++;
                    }
                    else
                    {
                        incomplete++;
                    }
                }
                
                _peerStatuses[infoHash].Complete = complete;
                _peerStatuses[infoHash].InComplete = incomplete;
            }
        }
    }
}