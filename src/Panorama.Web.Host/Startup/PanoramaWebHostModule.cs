using Abp.Modules;
using Abp.Reflection.Extensions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Panorama.Configuration;

namespace Panorama.Web.Host.Startup
{
    [DependsOn(
       typeof(PanoramaWebCoreModule))]
    public class PanoramaWebHostModule: AbpModule
    {
        private readonly IWebHostEnvironment _env;
        private readonly IConfigurationRoot _appConfiguration;

        public PanoramaWebHostModule(IWebHostEnvironment env)
        {
            _env = env;
            _appConfiguration = env.GetAppConfiguration();
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(PanoramaWebHostModule).GetAssembly());
        }
    }
}
