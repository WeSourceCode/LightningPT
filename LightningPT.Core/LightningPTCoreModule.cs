using System;
using Abp;
using Abp.Modules;

namespace LightningPT.Core
{
    [DependsOn(typeof(AbpKernelModule))]
    public class LightningPTCoreModule : AbpModule
    {
        
    }
}