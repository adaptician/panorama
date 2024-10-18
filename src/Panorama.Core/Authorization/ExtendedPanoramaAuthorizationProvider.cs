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

        #region Simulations

        var simulations = context.CreatePermission(PermissionNames.Pages_Tenant_Simulations, 
            L("Permission_Simulations"), 
            multiTenancySides: MultiTenancySides.Tenant, 
            featureDependency: new SimpleFeatureDependency(PanoramaFeatures.SimulationsFeature));

        #region Scenes

        var scenes = simulations.CreateChildPermission(PermissionNames.Pages_Tenant_Simulations_Scenes, 
            L("Permission_Simulations_Scenes"), 
            multiTenancySides: MultiTenancySides.Tenant, 
            featureDependency: new SimpleFeatureDependency(PanoramaFeatures.SimulationsFeature));

        #region CRUD

        scenes.CreateChildPermission(PermissionNames.Pages_Tenant_Simulations_Scenes_View, 
            L("Permission_Simulations_Scenes_View"), 
            multiTenancySides: MultiTenancySides.Tenant, 
            featureDependency: new SimpleFeatureDependency(PanoramaFeatures.SimulationsFeature));
        
        scenes.CreateChildPermission(PermissionNames.Pages_Tenant_Simulations_Scenes_Create, 
            L("Permission_Simulations_Scenes_Create"), 
            multiTenancySides: MultiTenancySides.Tenant, 
            featureDependency: new SimpleFeatureDependency(PanoramaFeatures.SimulationsFeature));
        
        scenes.CreateChildPermission(PermissionNames.Pages_Tenant_Simulations_Scenes_Update, 
            L("Permission_Simulations_Scenes_Update"), 
            multiTenancySides: MultiTenancySides.Tenant, 
            featureDependency: new SimpleFeatureDependency(PanoramaFeatures.SimulationsFeature));
        
        scenes.CreateChildPermission(PermissionNames.Pages_Tenant_Simulations_Scenes_Delete, 
            L("Permission_Simulations_Scenes_Delete"), 
            multiTenancySides: MultiTenancySides.Tenant, 
            featureDependency: new SimpleFeatureDependency(PanoramaFeatures.SimulationsFeature));

        #endregion

        #endregion

        #endregion
    }
}