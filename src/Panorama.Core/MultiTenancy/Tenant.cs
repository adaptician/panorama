using System;
using System.ComponentModel.DataAnnotations;
using Abp.MultiTenancy;
using Panorama.Authorization.Users;

namespace Panorama.MultiTenancy
{
    public class Tenant : AbpTenant<User>
    {
        #region Custom Properties.
        
        [MaxLength(TenantConstants.MaxCorrelationIdLength)]
        public string CorrelationId { get; set; }

        #endregion
        
        public Tenant()
        {
        }

        public Tenant(string tenancyName, string name)
            : base(tenancyName, name)
        {
            CorrelationId = Guid.NewGuid().ToString();
        }
    }
}
