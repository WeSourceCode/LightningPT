using Abp.Application.Services.Dto;
using Abp.AutoMapper;

namespace LightningPT.Application.BackendWebSiteService.UserManagementService.Dtos.CrudDtos
{
    [AutoMapTo(typeof(PtUserDto))]
    public class UpdateInput : CreateInput,IEntityDto<long>
    {
        public long Id { get; set; }
        
        /// <summary>
        /// 当前用户是否被禁止登录。
        /// </summary>
        public bool IsBanned { get; set; }
    }
}