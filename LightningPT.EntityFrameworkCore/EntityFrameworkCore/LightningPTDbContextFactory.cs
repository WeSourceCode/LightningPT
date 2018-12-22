using LightningPT.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace LightningPT.EntityFrameworkCore.EntityFrameworkCore
{
    /* 本工厂类的作用是在用户执行 EF 命令行工具进行迁移的时候进行使用。 */
    public class LightningPTDbContextFactory : IDesignTimeDbContextFactory<LightningPTDbContext>
    {
        public LightningPTDbContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<LightningPTDbContext>();
            var connectString = LightningPTCoreConsts.DefaultConnectionString;

            LightningPTDbContextConfigurer<LightningPTDbContext>.Configure(builder,connectString);
            
            return new LightningPTDbContext(builder.Options);
        }
    }
}