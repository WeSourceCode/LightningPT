using System.ComponentModel.DataAnnotations;
using Abp.AutoMapper;

namespace LightningPT.Application.BackendWebSiteService.UserManagementService.Dtos.CrudDtos
{
    [AutoMapTo(typeof(PtUserDto))]
    public class CreateInput
    {
        /// <summary>
        /// PT 站用户的登录名/帐号。
        /// </summary>
        [Required]
        public string UserName { get; set; }

        /// <summary>
        /// 用户的密码，采用密文进行存储。
        /// </summary>
        [Required]
        public string Password { get; set; }

        /// <summary>
        /// 用户的昵称，可以自定义，但是不能够重复。
        /// </summary>
        [Required]
        public string DisplayName { get; set; }

        /// <summary>
        /// 用户等级，参考等级表数据。
        /// </summary>
        [Required]
        public int Level { get; set; }
        
        /// <summary>
        /// 用户的魔力值，采用特定的算法进行计算。
        /// </summary>
        [Required]
        public long MagicValue { get; set; }
    }
}