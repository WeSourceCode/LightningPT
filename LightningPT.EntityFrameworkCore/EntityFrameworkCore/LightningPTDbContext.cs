using Abp.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace LightningPT.EntityFrameworkCore.EntityFrameworkCore
{
    public class LightningPTDbContext : AbpDbContext
    {
        public LightningPTDbContext(DbContextOptions options) : base(options)
        {
        }
    }
}