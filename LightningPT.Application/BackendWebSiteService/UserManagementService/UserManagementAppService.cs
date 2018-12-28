using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Domain.Entities;
using LightningPT.Application.BackendWebSiteService.UserManagementService.Dtos.CrudDtos;

namespace LightningPT.Application.BackendWebSiteService.UserManagementService
{
    public class UserManagementAppService : ApplicationService,IUserManagementAppService
    {
        public Task<PtUserDto> CreateUser(CreateInput input)
        {
            throw new System.NotImplementedException();
        }

        public Task<PtUserDto> UpdateUser(UpdateInput input)
        {
            throw new System.NotImplementedException();
        }

        public Task DeleteUser(UpdateInput input)
        {
            throw new System.NotImplementedException();
        }

        public Task GetUser(IEntity<long> input)
        {
            throw new System.NotImplementedException();
        }

        public Task<PagedResultDto<PtUserDto>> GetUserList(GetUserListInput input)
        {
            throw new System.NotImplementedException();
        }
    }
}