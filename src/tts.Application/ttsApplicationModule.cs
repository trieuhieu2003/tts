using Abp.AutoMapper;
using Abp.Modules;
using Abp.Reflection.Extensions;
using tts.Authorization;

namespace tts
{
    [DependsOn(
        typeof(ttsCoreModule), 
        typeof(AbpAutoMapperModule))]
    public class ttsApplicationModule : AbpModule
    {
        public override void PreInitialize()
        {
            Configuration.Authorization.Providers.Add<ttsAuthorizationProvider>();
        }

        public override void Initialize()
        {
            var thisAssembly = typeof(ttsApplicationModule).GetAssembly();

            IocManager.RegisterAssemblyByConvention(thisAssembly);

            Configuration.Modules.AbpAutoMapper().Configurators.Add(
                // Scan the assembly for classes which inherit from AutoMapper.Profile
                cfg => cfg.AddMaps(thisAssembly)
            );
        }
    }
}
