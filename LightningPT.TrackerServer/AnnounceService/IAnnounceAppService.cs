using System.Threading.Tasks;
using LightningPT.TrackerServer.AnnounceService.Dtos.InputDto;

namespace LightningPT.TrackerServer.AnnounceService
{
    public interface IAnnounceAppService
    {
        Task GetTrackInfo(GetTrackInfoInput input);
    }
}