using Abp.AspNetCore.Mvc.Controllers;
using Abp.IdentityFramework;
using Microsoft.AspNetCore.Identity;

namespace Panorama.Controllers
{
    public abstract class PanoramaControllerBase: AbpController
    {
        protected PanoramaControllerBase()
        {
            LocalizationSourceName = PanoramaConsts.LocalizationSourceName;
        }

        protected void CheckErrors(IdentityResult identityResult)
        {
            identityResult.CheckErrors(LocalizationManager);
        }
    }
}
