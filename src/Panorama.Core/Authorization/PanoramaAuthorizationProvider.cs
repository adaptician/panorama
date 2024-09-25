using System;
using Abp.Authorization;
using Abp.Localization;
using Abp.MultiTenancy;

namespace Panorama.Authorization
{
    [Obsolete("ExtendedPanoramaAuthorizationProvider replaces PanoramaAuthorizationProvider - please refrain from customizing boilerplate files.")]
    public class PanoramaAuthorizationProvider : AuthorizationProvider
    {
        public override void SetPermissions(IPermissionDefinitionContext context)
        {
            context.CreatePermission(PermissionNames.Pages_Users, L("Users"));
            context.CreatePermission(PermissionNames.Pages_Users_Activation, L("UsersActivation"));
            context.CreatePermission(PermissionNames.Pages_Roles, L("Roles"));
            context.CreatePermission(PermissionNames.Pages_Tenants, L("Tenants"), multiTenancySides: MultiTenancySides.Host);
        }

        protected static ILocalizableString L(string name)
        {
            return new LocalizableString(name, PanoramaConsts.LocalizationSourceName);
        }
    }
}
