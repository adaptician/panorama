using Abp.Application.Features;
using Abp.Localization;
using Abp.UI.Inputs;

namespace Panorama.Features;

public class PanoramaFeatureProvider : FeatureProvider
{
    public override void SetFeatures(IFeatureDefinitionContext context)
    {
        #region Scenes

        context.Create(
            PanoramaFeatures.ScenesFeature,
            defaultValue: "true",
            displayName: L("ScenesFeature"),
            description: L("ScenesFeatureDescription"),
            inputType: new CheckboxInputType(),
            scope: FeatureScopes.Tenant
        );

        #endregion

        #region Simulations

        var simulations = context.Create(
            PanoramaFeatures.SimulationsFeature,
            defaultValue: "true",
            displayName: L("SimulationsFeature"),
            description: L("SimulationsFeatureDescription"),
            inputType: new CheckboxInputType(),
            scope: FeatureScopes.Tenant
        );

        simulations.CreateChildFeature(
            PanoramaFeatures.SimulationRunningFeature,
            defaultValue: "true",
            displayName: L("SimulationRunningFeature"),
            description: L("SimulationRunningFeatureDescription"),
            inputType: new CheckboxInputType(),
            scope: FeatureScopes.Tenant
        );

        #endregion
    }
    
    protected static ILocalizableString L(string name)
    {
        return new LocalizableString(name, PanoramaConsts.LocalizationSourceName);
    }
}