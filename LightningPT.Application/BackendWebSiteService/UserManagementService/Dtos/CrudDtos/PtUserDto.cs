using System;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;

namespace LightningPT.Application.BackendWebSiteService.UserManagementService.Dtos.CrudDtos
{
    [AutoMapTo(typeof(PtUserDto))]
    [AutoMapFrom(typeof(PtUserDto))]
    public class PtUserDto : EntityDto<long>
    {
        /// <summary>
        /// PT 站用户的登录名/帐号。
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 用户的密码，采用密文进行存储。
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// 用户的昵称，可以自定义，但是不能够重复。
        /// </summary>
        public string DisplayName { get; set; }

        /// <summary>
        /// 用户等级，参考等级表数据。
        /// </summary>
        public int Level { get; set; }

        /// <summary>
        /// 用户的魔力值，采用特定的算法进行计算。
        /// </summary>
        public long MagicValue { get; set; }

        /// <summary>
        /// 最后登录的 IP
        /// </summary>
        public string LastLoginIp { get; set; }

        /// <summary>
        /// 最后登录的时间
        /// </summary>
        public DateTime? LasLoginDateTime { get; set; }

        /// <summary>
        /// 当前用户是否被禁止登录。
        /// </summary>
        public bool IsBanned { get; set; }

        /// <summary>
        /// 被拉入小小黑屋的时间，可能为 NULL。
        /// </summary>
        public DateTime? BannedTime { get; set; }

        /// <summary>
        /// 用户的唯一密钥，根据用户的登录名+密码+昵称进行 Hash 计算之后的唯一值。
        /// </summary>
        public string PassKey { get; set; }
    }
}