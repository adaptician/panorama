using Abp.Authorization;
using Panorama.Authorization.Roles;
using Panorama.Authorization.Users;

namespace Panorama.Authorization
{
    public class PermissionChecker : PermissionChecker<Role, User>
    {
        public PermissionChecker(UserManager userManager)
            : base(userManager)
        {
        }
    }
}
