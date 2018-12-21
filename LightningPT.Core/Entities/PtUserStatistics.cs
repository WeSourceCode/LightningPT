using System;
using Abp.Domain.Entities;

namespace LightningPT.Core.Entities
{
    public class PtUserStatistics : Entity<Guid>
    {
        /// <summary>
        /// 用户的 Id。
        /// </summary>
        public long UserId { get; set; }

        /// <summary>
        /// 用户总共上传的数据量，单位为 Byte。
        /// </summary>
        public long UploadTotalCount { get; set; }

        /// <summary>
        /// 用户总共下载的数据量，单位为 Byte。
        /// </summary>
        public long DownloadTotalCount { get; set; }

        /// <summary>
        /// 用户的种子分享率。
        /// </summary>
        public double SharingRate { get; set; }
    }
}