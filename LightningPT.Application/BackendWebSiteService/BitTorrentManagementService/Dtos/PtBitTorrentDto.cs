using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using LightningPT.Core.Entities;

namespace LightningPT.Application.BackendWebSiteService.BitTorrentManagementService.Dtos
{
    [AutoMapFrom(typeof(PtBitTorrent))]
    [AutoMapTo(typeof(PtBitTorrent))]
    public class PtBitTorrentDto : EntityDto<int>
    {
        
    }
}