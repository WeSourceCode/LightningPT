using System;
using Abp.Domain.Entities;

namespace LightningPT.Core.Entities
{
    /// <summary>
    /// 用户上传的种子实体定义。
    /// </summary>
    public class PtBitTorrent : Entity<int>
    {
        /// <summary>
        /// 上传种子用户的用户。
        /// </summary>
        public long Uploader { get; set; }

        /// <summary>
        /// BT 种子包含的文件大小
        /// </summary>
        public long Size { get; set; }
        
        /// <summary>
        /// 类别，关联的种子类别表 Id
        /// </summary>
        public int CateGory { get; set; }

        /// <summary>
        /// 是否匿名上传
        /// </summary>
        public bool IsAnonymous { get; set; }

        /// <summary>
        /// 种子的唯一 Hash 标识。
        /// </summary>
        public string InfoHash { get; set; }

        /// <summary>
        /// 种子上传时间。
        /// </summary>
        public DateTime UploadTime { get; set; }

        /// <summary>
        /// 种子下载量统计。
        /// </summary>
        public long DownLoadCount { get; set; }
    }
}