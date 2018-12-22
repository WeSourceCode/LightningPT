using Abp.EntityFrameworkCore;
using Abp.EntityFrameworkCore.Configuration;
using Abp.Modules;
using LightningPT.Core;
using LightningPT.EntityFrameworkCore.EntityFrameworkCore;

namespace LightningPT.EntityFrameworkCore
{
    [DependsOn(typeof(AbpEntityFrameworkCoreModule),
        typeof(LightningPTCoreModule))]
    public class LightningPTEntityFrameworkCoreModule : AbpModule
    {
        public override void PreInitialize()
        {
            Configuration.Modules.AbpEfCore().AddDbContext<LightningPTDbContext>(options =>
            {
                if (options.ExistingConnection != null)
                {
                    LightningPTDbContextConfigurer<LightningPTDbContext>.Configure(options.DbContextOptions,options.ExistingConnection);
                }
                else
                {
                    LightningPTDbContextConfigurer<LightningPTDbContext>.Configure(options.DbContextOptions,options.ConnectionString);
                }
            });
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(LightningPTEntityFrameworkCoreModule).Assembly);
        }
    }
}