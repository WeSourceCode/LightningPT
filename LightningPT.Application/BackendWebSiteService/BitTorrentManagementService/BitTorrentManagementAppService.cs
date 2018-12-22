using Abp.Application.Services;
using Abp.Domain.Repositories;
using LightningPT.Application.BackendWebSiteService.BitTorrentManagementService.Dtos;
using LightningPT.Application.BackendWebSiteService.BitTorrentManagementService.Dtos.CrudDtos;
using LightningPT.Core.Entities;

namespace LightningPT.Application.BackendWebSiteService.BitTorrentManagementService
{
    /// <summary>
    /// 后台种子管理服务，管理当前 PT 站用户上传的种子信息。
    /// </summary>
    public class BitTorrentManagementAppService : AsyncCrudAppService<PtBitTorrent,PtBitTorrentDto,int,GetAllInput,CreateInput,UpdateInput>,IBitTorrentManagementAppService
    {
        public BitTorrentManagementAppService(IRepository<PtBitTorrent, int> repository) : base(repository)
        {
        }
    }
}