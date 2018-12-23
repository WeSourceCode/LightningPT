using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Dependency;
using BencodeNET.Objects;
using LightningPT.Core.User;

namespace LightningPT.TrackerServer.BitTorrentTracker
{
    public class Tracker : ITracker,ISingletonDependency
    {
        private readonly IBitTorrentManager _bitTorrentManager;
        private readonly ITorrentChecker _torrentChecker;

        private readonly Random _random;

        public TimeSpan AnnounceInterval { get; set; }
        public TimeSpan MinAnnounceInterval { get; set; }
        public TimeSpan TimeoutInterval { get; set; }
        public BString TrackerId { get; set; }
        
        public Tracker(IBitTorrentManager bitTorrentManager, ITorrentChecker torrentChecker)
        {
            _bitTorrentManager = bitTorrentManager;
            _torrentChecker = torrentChecker;
            
            _random = new Random();
            
            AnnounceInterval = TimeSpan.FromMinutes(30);
            MinAnnounceInterval = TimeSpan.FromSeconds(30);
            TimeoutInterval = TimeSpan.FromMinutes(5);
            TrackerId = new BString("LightningPT-Tracker");
        }

        public async Task<BDictionary> HandleRequest(AnnounceRequestParameters request)
        {
            if (request.Error.Count != 0) return request.Error;
            
            var resultDict = new BDictionary();
            
            if (!await _torrentChecker.TorrentIsValidAsync(request.InfoHash))
            {
                resultDict.Add(LightningPTTrackerServerConsts.FailureKey,new BString("请求的种子无效。"));
                return resultDict;
            }

            _bitTorrentManager.UpdateInfo(request);
            _bitTorrentManager.ClearZombiePeers(request.InfoHash,TimeSpan.FromMinutes(10));
            var peers = _bitTorrentManager.GetPeers(request.InfoHash);
            
            await HandlePeersData(resultDict, peers, request);
            
            resultDict.Add(LightningPTTrackerServerConsts.IntervalKey,new BNumber((int)AnnounceInterval.TotalSeconds));
            resultDict.Add(LightningPTTrackerServerConsts.MinIntervalKey,new BNumber((int)MinAnnounceInterval.TotalSeconds));
            resultDict.Add(LightningPTTrackerServerConsts.TrackerIdKey,TrackerId);
            resultDict.Add(LightningPTTrackerServerConsts.CompleteKey,new BNumber(_bitTorrentManager.GetComplete(request.InfoHash)));
            resultDict.Add(LightningPTTrackerServerConsts.IncompleteKey,new BNumber(_bitTorrentManager.GetInComplete(request.InfoHash)));
            
            return resultDict;
        }

        private Task HandlePeersData(BDictionary resultDict,IReadOnlyList<Peer> peers, AnnounceRequestParameters request)
        {
            var total = Math.Min(peers.Count, request.PeerWantCount);
            var start = _random.Next(0, peers.Count);
            
            if (request.IsEnableCompact)
            {
                var compactResponse = new byte[total * 6];

                for (; total > 0; total--)
                {
                    var currentPeer = peers[(start++) % peers.Count];
                    Buffer.BlockCopy(currentPeer.ToBytes(),0,compactResponse,(total - 1) * 6,6);
                }
                
                resultDict.Add(LightningPTTrackerServerConsts.PeersKey,new BString(compactResponse));
                return Task.FromResult(resultDict);
            }
            else
            {
                
                var nonCompactResponse = new BList();

                for (; total > 0; total--)
                {
                    var currentPeer = peers[(start++) % peers.Count];
                    nonCompactResponse.Add(currentPeer.ToBEncodedDictionary());
                }
                
                resultDict.Add(LightningPTTrackerServerConsts.PeersKey,nonCompactResponse);
                return Task.FromResult(resultDict);
            }
        }
    }
}