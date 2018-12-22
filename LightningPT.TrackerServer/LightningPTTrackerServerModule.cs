using Abp;
using Abp.Modules;
using LightningPT.Core;

namespace LightningPT.TrackerServer
{
    [DependsOn(typeof(LightningPTCoreModule))]
    public class LightningPTTrackerServerModule : AbpModule
    {
        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(LightningPTTrackerServerModule).Assembly);
        }
    }
}