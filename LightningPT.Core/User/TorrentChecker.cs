using System.Linq;
using System.Threading.Tasks;
using Abp.Dependency;
using Abp.Domain.Repositories;
using LightningPT.Core.Entities;

namespace LightningPT.Core.User
{
    public class TorrentChecker : ITorrentChecker,ITransientDependency
    {
        private readonly IRepository<PtBitTorrent> _torrentRep;

        public TorrentChecker(IRepository<PtBitTorrent> torrentRep)
        {
            _torrentRep = torrentRep;
        }

        public Task<bool> TorrentIsValidAsync(string infoHash)
        {
            return Task.FromResult(_torrentRep.GetAll().Any(z=>z.InfoHash == infoHash));
        }
    }
}