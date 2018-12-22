using Abp.AutoMapper;
using Abp.Modules;
using LightningPT.Core;

namespace LightningPT.Application
{
    [DependsOn(typeof(LightningPTCoreModule),typeof(AbpAutoMapperModule))]
    public class LightningPTApplicationModule : AbpModule
    {
        
    }
}