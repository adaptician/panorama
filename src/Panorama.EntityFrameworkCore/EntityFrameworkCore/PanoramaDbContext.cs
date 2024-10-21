using Microsoft.EntityFrameworkCore;
using Abp.Zero.EntityFrameworkCore;
using Panorama.Authorization.Roles;
using Panorama.Authorization.Users;
using Panorama.MultiTenancy;
using Panorama.Simulations;

namespace Panorama.EntityFrameworkCore
{
    public class PanoramaDbContext : AbpZeroDbContext<Tenant, Role, User, PanoramaDbContext>
    {
        /* Define a DbSet for each entity of the application */
        
        public virtual DbSet<Simulation> Simulations { get; set; }
        
        public PanoramaDbContext(DbContextOptions<PanoramaDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            #region Tenants

            modelBuilder.Entity<Tenant>(b =>
            {
                b.HasIndex(e => new { e.CorrelationId }).IsUnique();
            });

            #endregion
            
            #region Users

            modelBuilder.Entity<User>(b =>
            {
                b.HasIndex(e => new { e.CorrelationId }).IsUnique();
            });

            #endregion

            #region Simulations

            modelBuilder.Entity<Simulation>(b =>
            {
                b.HasIndex(e => new { e.Name }).IsUnique();
            });

            #endregion
        }
    }
}
