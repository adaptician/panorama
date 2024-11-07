using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Abp.Authorization.Users;
using Abp.Extensions;
using Panorama.Core.Shared.Users;

namespace Panorama.Authorization.Users
{
    public class User : AbpUser<User>
    {
        public const string DefaultPassword = "123qwe";

        #region Custom Properties.
        
        [MaxLength(UserConstants.MaxCorrelationIdLength)]
        public string CorrelationId { get; set; }

        #endregion

        public static string CreateRandomPassword()
        {
            return Guid.NewGuid().ToString("N").Truncate(16);
        }

        public static User CreateTenantAdminUser(int tenantId, string emailAddress)
        {
            var user = new User
            {
                TenantId = tenantId,
                UserName = AdminUserName,
                Name = AdminUserName,
                Surname = AdminUserName,
                EmailAddress = emailAddress,
                Roles = new List<UserRole>(),
                
                // Custom assignments.
                CorrelationId = Guid.NewGuid().ToString()
            };

            user.SetNormalizedNames();

            return user;
        }
    }
}
