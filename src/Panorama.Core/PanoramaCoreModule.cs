using Abp.Dependency;
using Abp.Localization;
using Abp.Modules;
using Abp.Reflection.Extensions;
using Abp.Runtime.Security;
using Abp.Timing;
using Abp.Zero;
using Abp.Zero.Configuration;
using Panorama.Authorization.Roles;
using Panorama.Authorization.Users;
using Panorama.Configuration;
using Panorama.Events;
using Panorama.Features;
using Panorama.Localization;
using Panorama.MultiTenancy;
using Panorama.Timing;

namespace Panorama
{
    [DependsOn(typeof(AbpZeroCoreModule))]
    public class PanoramaCoreModule : AbpModule
    {
        public override void PreInitialize()
        {
            Configuration.Auditing.IsEnabledForAnonymousUsers = true;

            // Declare entity types
            Configuration.Modules.Zero().EntityTypes.Tenant = typeof(Tenant);
            Configuration.Modules.Zero().EntityTypes.Role = typeof(Role);
            Configuration.Modules.Zero().EntityTypes.User = typeof(User);

            PanoramaLocalizationConfigurer.Configure(Configuration.Localization);

            // Enable this line to create a multi-tenant application.
            Configuration.MultiTenancy.IsEnabled = PanoramaConsts.MultiTenancyEnabled;

            // Configure roles
            AppRoleConfig.Configure(Configuration.Modules.Zero().RoleManagement);

            Configuration.Settings.Providers.Add<AppSettingProvider>();
            
            Configuration.Localization.Languages.Add(new LanguageInfo("fa", "فارسی", "famfamfam-flags ir"));
            
            Configuration.Settings.SettingEncryptionConfiguration.DefaultPassPhrase = PanoramaConsts.DefaultPassPhrase;
            SimpleStringCipher.DefaultPassPhrase = PanoramaConsts.DefaultPassPhrase;
            
            // Register custom services.
            PreInitializeCustom();
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(PanoramaCoreModule).GetAssembly());
        }

        public override void PostInitialize()
        {
            IocManager.Resolve<AppTimes>().StartupTime = Clock.Now;
        }

        private void PreInitializeCustom()
        {
            Configuration.Features.Providers.Add<PanoramaFeatureProvider>();
        }
    }
}
