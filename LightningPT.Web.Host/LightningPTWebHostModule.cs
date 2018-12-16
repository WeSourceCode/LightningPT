using Abp.Configuration.Startup;
using Abp.Modules;
 
 namespace LightningPT.Web.Host
 {
     public class LightningPTWebHostModule : AbpModule
     {
         public override void PreInitialize()
         {
             Configuration.Auditing.IsEnabled = false;
             Configuration.Modules.AbpWebCommon().SendAllExceptionsToClients = true;
         }
     }
 }