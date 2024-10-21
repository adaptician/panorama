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

        #region Scenes

        var scenes = context.CreatePermission(PermissionNames.Pages_Tenant_Scenes, 
            L("Permission_Scenes"), 
            multiTenancySides: MultiTenancySides.Tenant, 
            featureDependency: new SimpleFeatureDependency(PanoramaFeatures.ScenesFeature));

        #region CRUD

        scenes.CreateChildPermission(PermissionNames.Pages_Tenant_Scenes_View, 
            L("Permission_Scenes_View"), 
            multiTenancySides: MultiTenancySides.Tenant, 
            featureDependency: new SimpleFeatureDependency(PanoramaFeatures.ScenesFeature));
        
        scenes.CreateChildPermission(PermissionNames.Pages_Tenant_Scenes_Create, 
            L("Permission_Scenes_Create"), 
            multiTenancySides: MultiTenancySides.Tenant, 
            featureDependency: new SimpleFeatureDependency(PanoramaFeatures.ScenesFeature));
        
        scenes.CreateChildPermission(PermissionNames.Pages_Tenant_Scenes_Update, 
            L("Permission_Scenes_Update"), 
            multiTenancySides: MultiTenancySides.Tenant, 
            featureDependency: new SimpleFeatureDependency(PanoramaFeatures.ScenesFeature));
        
        scenes.CreateChildPermission(PermissionNames.Pages_Tenant_Scenes_Delete, 
            L("Permission_Scenes_Delete"), 
            multiTenancySides: MultiTenancySides.Tenant, 
            featureDependency: new SimpleFeatureDependency(PanoramaFeatures.ScenesFeature));

        #endregion

        #endregion
        
        #region Simulations

        var simulations = context.CreatePermission(PermissionNames.Pages_Tenant_Simulations, 
            L("Permission_Simulations"), 
            multiTenancySides: MultiTenancySides.Tenant, 
            featureDependency: new SimpleFeatureDependency(PanoramaFeatures.SimulationsFeature));

        #endregion
    }
}