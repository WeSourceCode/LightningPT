using System.Threading.Tasks;

namespace LightningPT.Core.User
{
    public interface ITorrentChecker
    {
        Task<bool> TorrentIsValidAsync(string infoHash);
    }
}