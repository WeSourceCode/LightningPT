using Abp.EntityFrameworkCore;
using LightningPT.Core.Entities;
using Microsoft.EntityFrameworkCore;

// ReSharper disable IdentifierTypo

namespace LightningPT.EntityFrameworkCore.EntityFrameworkCore
{
    public class LightningPTDbContext : AbpDbContext
    {
        public DbSet<PtBitTorrent> PtBitTorrents { get; set; }

        public DbSet<PtBitTorrentCateGory> PtBitTorrentCateGories { get; set; }

        public DbSet<PtUser> PtUsers { get; set; }

        public DbSet<PtUserLevel> PtUserLevels { get; set; }

        public DbSet<PtUserStatistics> PtUserStatisticses { get; set; }
        
        public LightningPTDbContext(DbContextOptions options) : base(options)
        {
        }
    }
}