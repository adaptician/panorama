using Abp.Application.Features;
using Abp.Authorization;
using Abp.MultiTenancy;
using Panorama.Features;

namespace Panorama.Authorization;

public class ExtendedPanoramaAuthorizationProvider : PanoramaAuthorizationProvider
{
    public override void SetPermissions(IPermissionDefinitionContext context)
    {
        base.SetPermissions(context);
        
        var simulations = context.CreatePermission(PermissionNames.Pages_Tenant_Simulations, 
            L("Permission_Simulations"), 
            multiTenancySides: MultiTenancySides.Tenant, 
            featureDependency: new SimpleFeatureDependency(PanoramaFeatures.SimulationsFeature));
    }
}