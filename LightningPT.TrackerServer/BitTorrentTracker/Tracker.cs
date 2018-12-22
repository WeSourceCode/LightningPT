using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Dependency;
using BencodeNET.Objects;
using LightningPT.Core.User;

namespace LightningPT.TrackerServer.BitTorrentTracker
{
    public class Tracker : ITracker,ISingletonDependency
    {
        private IBitTorrentManager _bitTorrentManager;
        private ITorrentChecker _torrentChecker;
        
        public Tracker(IBitTorrentManager bitTorrentManager, ITorrentChecker torrentChecker)
        {
            _bitTorrentManager = bitTorrentManager;
            _torrentChecker = torrentChecker;
        }

        public async Task<BDictionary> HandleRequest(AnnounceRequestParameters request)
        {
            var resultDict = new BDictionary();
            
            if (!await _torrentChecker.TorrentIsValidAsync(request.InfoHash))
            {
                resultDict.Add(LightningPTTrackerServerConsts.FailureKey,new BString("请求的种子无效。"));
                return resultDict;
            }

            var peers = _bitTorrentManager.GetPeers(request.InfoHash);
            
            return resultDict;
        }

        private Task<BDictionary> HandlePeersData(IReadOnlyList<Peer> peers)
        {
            return null;
        }
    }
}