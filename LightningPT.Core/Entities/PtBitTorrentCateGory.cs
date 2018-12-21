using Abp.Domain.Entities;

namespace LightningPT.Core.Entities
{
    public class PtBitTorrentCateGory : Entity<int>
    {
        public string DisplayName { get; set; }

        public string Code { get; set; }

        public string IconUrl { get; set; }

        public string Description { get; set; }
    }
}