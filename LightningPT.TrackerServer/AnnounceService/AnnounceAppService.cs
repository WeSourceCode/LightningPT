using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.UI;
using LightningPT.Core.User;
using LightningPT.TrackerServer.AnnounceService.Dtos.InputDto;
using LightningPT.TrackerServer.BitTorrentTracker;

namespace LightningPT.TrackerServer.AnnounceService
{
    /// <summary>
    /// Tracker 服务器监听服务。
    /// </summary>
    public class AnnounceAppService : ApplicationService,IAnnounceAppService
    {
        private readonly ITracker _tracker;
        private readonly IUserKeyChecker _keyChecker;

        public AnnounceAppService(ITracker tracker,
            IUserKeyChecker keyChecker)
        {
            _tracker = tracker;
            _keyChecker = keyChecker;
        }

        public async Task GetTrackInfo(GetTrackInfoInput input)
        {
            if (!await _keyChecker.PassKeyIsValidAsync(input.PassKey))
            {
                throw new UserFriendlyException($"无效的 PassKey: {input.PassKey} 。");
            }
            
            var result = await _tracker.HandleRequest(input);
        }
    }
}