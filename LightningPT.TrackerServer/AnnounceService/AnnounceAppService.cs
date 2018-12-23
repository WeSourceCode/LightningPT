using System.Net;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.UI;
using Abp.Web.Models;
using LightningPT.Core.User;
using LightningPT.TrackerServer.AnnounceService.Dtos.InputDto;
using LightningPT.TrackerServer.BitTorrentTracker;
using Microsoft.AspNetCore.Http;

namespace LightningPT.TrackerServer.AnnounceService
{
    /// <summary>
    /// Tracker 服务器监听服务。
    /// </summary>
    public class AnnounceAppService : ApplicationService,IAnnounceAppService
    {
        private readonly ITracker _tracker;
        private readonly IUserKeyChecker _keyChecker;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AnnounceAppService(ITracker tracker,
            IUserKeyChecker keyChecker,
            IHttpContextAccessor httpContextAccessor)
        {
            _tracker = tracker;
            _keyChecker = keyChecker;
            _httpContextAccessor = httpContextAccessor;
        }

        [DontWrapResult]
        public async Task GetTrackInfo(GetTrackInfoInput input)
        {
            if (!await _keyChecker.PassKeyIsValidAsync(input.PassKey))
            {
                throw new UserFriendlyException($"无效的 PassKey: {input.PassKey} 。");
            }

            if (string.IsNullOrEmpty(input.Ip))
            {
                input.Ip = _httpContextAccessor.HttpContext.Connection.RemoteIpAddress.MapToIPv4().ToString();
            }
            
            var result = await _tracker.HandleRequest(input);

            var response = _httpContextAccessor.HttpContext.Response;
            // 构建返回值信息
            var resultBytes = result.EncodeAsBytes();
            response.ContentType = "text/plain";
            response.StatusCode = 200;
            response.ContentLength = resultBytes.Length;
            await response.Body.WriteAsync(resultBytes);
        }
    }
}