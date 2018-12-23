using Abp.AspNetCore;
using Abp.AspNetCore.Configuration;
using Abp.Configuration.Startup;
using Abp.Modules;
using LightningPT.Application;
using LightningPT.Core;
using LightningPT.EntityFrameworkCore;
using LightningPT.TrackerServer;

namespace LightningPT.Web.Host
 {
     [DependsOn(typeof(LightningPTApplicationModule),
         typeof(LightningPTEntityFrameworkCoreModule),
         typeof(LightningPTTrackerServerModule),
         typeof(AbpAspNetCoreModule))]
     public class LightningPTWebHostModule : AbpModule
     {
         public override void PreInitialize()
         {
             ConfigureAspNetCoreWeb();
             ConfigureDataBase();
         }

         private void ConfigureAspNetCoreWeb()
         {
             Configuration.Modules.AbpWebCommon().SendAllExceptionsToClients = true;
             Configuration.Modules.AbpAspNetCore().CreateControllersForAppServices(typeof(LightningPTApplicationModule).Assembly);
             Configuration.Modules.AbpAspNetCore().CreateControllersForAppServices(typeof(LightningPTTrackerServerModule).Assembly);
         }

         private void ConfigureDataBase()
         {
             Configuration.DefaultNameOrConnectionString = LightningPTCoreConsts.DefaultConnectionString;
         }
     }
 }