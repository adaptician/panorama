using Abp.AutoMapper;
using Abp.Modules;
using Abp.Reflection.Extensions;
using Panorama.Authorization;

namespace Panorama
{
    [DependsOn(
        typeof(PanoramaCoreModule), 
        typeof(AbpAutoMapperModule))]
    public class PanoramaApplicationModule : AbpModule
    {
        public override void PreInitialize()
        {
            Configuration.Authorization.Providers.Add<PanoramaAuthorizationProvider>();
            
            // Register custom services.
            PreInitializeCustom();
        }

        public override void Initialize()
        {
            var thisAssembly = typeof(PanoramaApplicationModule).GetAssembly();

            IocManager.RegisterAssemblyByConvention(thisAssembly);

            Configuration.Modules.AbpAutoMapper().Configurators.Add(
                // Scan the assembly for classes which inherit from AutoMapper.Profile
                cfg => cfg.AddMaps(thisAssembly)
            );
        }

        private void PreInitializeCustom()
        {
            Configuration.Authorization.Providers.Remove<PanoramaAuthorizationProvider>();
            Configuration.Authorization.Providers.Add<ExtendedPanoramaAuthorizationProvider>();
        }
    }
}
