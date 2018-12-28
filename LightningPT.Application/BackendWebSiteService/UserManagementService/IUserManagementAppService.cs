using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Domain.Entities;
using LightningPT.Application.BackendWebSiteService.UserManagementService.Dtos.CrudDtos;

namespace LightningPT.Application.BackendWebSiteService.UserManagementService
{
    public interface IUserManagementAppService : IApplicationService
    {
        Task<PtUserDto> CreateUser(CreateInput input);

        Task<PtUserDto> UpdateUser(UpdateInput input);

        Task DeleteUser(UpdateInput input);

        Task GetUser(IEntity<long> input);

        Task<PagedResultDto<PtUserDto>> GetUserList(GetUserListInput input);
    }
}