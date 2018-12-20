using LightningPT.TrackerServer.AnnounceService.Dtos.InputDto;

namespace LightningPT.TrackerServer.AnnounceService
{
    public interface IAnnounceAppService
    {
        void GetTrackInfo(GetTrackInfoInput input);
    }
}