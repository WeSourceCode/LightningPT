using System.ComponentModel.DataAnnotations;
using Abp.Domain.Entities;

namespace LightningPT.Core.Entities
{
    /// <summary>
    /// PT 站用户等级定义。
    /// </summary>
    public class PtUserLevel : Entity<int>
    {
        [StringLength(50)]
        public string DisplayName { get; set; }
        
        [StringLength(10)]
        public string Code { get; set; }

        [StringLength(200)]
        public string IconUrl { get; set; }

        [StringLength(1000)]
        public string Description { get; set; }
    }
}