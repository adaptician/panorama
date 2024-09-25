using Abp.Application.Features;
using Abp.Localization;
using Abp.UI.Inputs;

namespace Panorama.Features;

public class PanoramaFeatureProvider : FeatureProvider
{
    public override void SetFeatures(IFeatureDefinitionContext context)
    {
        context.Create(
            PanoramaFeatures.SimulationsFeature,
            defaultValue: "true",
            displayName: L("SimulationsFeature"),
            description: L("SimulationsFeatureDescription"),
            inputType: new CheckboxInputType(),
            scope: FeatureScopes.Tenant
        );
    }
    
    protected static ILocalizableString L(string name)
    {
        return new LocalizableString(name, PanoramaConsts.LocalizationSourceName);
    }
}