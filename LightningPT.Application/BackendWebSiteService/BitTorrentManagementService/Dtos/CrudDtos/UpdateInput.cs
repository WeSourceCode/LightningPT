using Abp.Application.Services.Dto;

namespace LightningPT.Application.BackendWebSiteService.BitTorrentManagementService.Dtos.CrudDtos
{
    public class UpdateInput : CreateInput,IEntityDto<int>
    {
        public int Id { get; set; }
    }
}