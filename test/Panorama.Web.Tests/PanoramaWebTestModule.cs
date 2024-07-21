using Abp.AspNetCore;
using Abp.AspNetCore.TestBase;
using Abp.Modules;
using Abp.Reflection.Extensions;
using Panorama.EntityFrameworkCore;
using Panorama.Web.Startup;
using Microsoft.AspNetCore.Mvc.ApplicationParts;

namespace Panorama.Web.Tests
{
    [DependsOn(
        typeof(PanoramaWebMvcModule),
        typeof(AbpAspNetCoreTestBaseModule)
    )]
    public class PanoramaWebTestModule : AbpModule
    {
        public PanoramaWebTestModule(PanoramaEntityFrameworkModule abpProjectNameEntityFrameworkModule)
        {
            abpProjectNameEntityFrameworkModule.SkipDbContextRegistration = true;
        } 
        
        public override void PreInitialize()
        {
            Configuration.UnitOfWork.IsTransactional = false; //EF Core InMemory DB does not support transactions.
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(PanoramaWebTestModule).GetAssembly());
        }
        
        public override void PostInitialize()
        {
            IocManager.Resolve<ApplicationPartManager>()
                .AddApplicationPartsIfNotAddedBefore(typeof(PanoramaWebMvcModule).Assembly);
        }
    }
}