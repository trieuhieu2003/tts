using Abp.AspNetCore;
using Abp.AspNetCore.TestBase;
using Abp.Modules;
using Abp.Reflection.Extensions;
using tts.EntityFrameworkCore;
using tts.Web.Startup;
using Microsoft.AspNetCore.Mvc.ApplicationParts;

namespace tts.Web.Tests
{
    [DependsOn(
        typeof(ttsWebMvcModule),
        typeof(AbpAspNetCoreTestBaseModule)
    )]
    public class ttsWebTestModule : AbpModule
    {
        public ttsWebTestModule(ttsEntityFrameworkModule abpProjectNameEntityFrameworkModule)
        {
            abpProjectNameEntityFrameworkModule.SkipDbContextRegistration = true;
        } 
        
        public override void PreInitialize()
        {
            Configuration.UnitOfWork.IsTransactional = false; //EF Core InMemory DB does not support transactions.
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(ttsWebTestModule).GetAssembly());
        }
        
        public override void PostInitialize()
        {
            IocManager.Resolve<ApplicationPartManager>()
                .AddApplicationPartsIfNotAddedBefore(typeof(ttsWebMvcModule).Assembly);
        }
    }
}