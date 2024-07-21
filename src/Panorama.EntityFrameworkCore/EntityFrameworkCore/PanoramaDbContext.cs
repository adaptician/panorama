using Microsoft.EntityFrameworkCore;
using Abp.Zero.EntityFrameworkCore;
using Panorama.Authorization.Roles;
using Panorama.Authorization.Users;
using Panorama.MultiTenancy;

namespace Panorama.EntityFrameworkCore
{
    public class PanoramaDbContext : AbpZeroDbContext<Tenant, Role, User, PanoramaDbContext>
    {
        /* Define a DbSet for each entity of the application */
        
        public PanoramaDbContext(DbContextOptions<PanoramaDbContext> options)
            : base(options)
        {
        }
    }
}
