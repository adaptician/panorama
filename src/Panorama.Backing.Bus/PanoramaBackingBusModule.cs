using Abp.Modules;
using Abp.Reflection.Extensions;

namespace Panorama.Backing.Bus;

public class PanoramaBackingBusModule : AbpModule
{
    public override void Initialize()
    {
        IocManager.RegisterAssemblyByConvention(typeof(PanoramaBackingBusModule).GetAssembly());
    }
}