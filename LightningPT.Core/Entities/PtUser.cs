using System;
using Abp.Domain.Entities;

namespace LightningPT.Core.Entities
{
    /// <summary>
    /// PT 站用户实体定义。
    /// </summary>
    public class PtUser : Entity<long>
    {
        /// <summary>
        /// 登录名
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// 昵称
        /// </summary>
        public string DisplayName { get; set; }

        /// <summary>
        /// 用户等级，参考等级表数据
        /// </summary>
        public int Level { get; set; }

        /// <summary>
        /// 魔力值
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
        /// 是否被禁止登录。
        /// </summary>
        public bool IsBanned { get; set; }

        /// <summary>
        /// 被拉入小小黑屋的时间，可能为 NULL。
        /// </summary>
        public DateTime? BannedTime { get; set; }

        /// <summary>
        /// 用户的唯一密钥
        /// </summary>
        public string PassKey { get; set; }
    }
}