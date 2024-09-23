using Microsoft.EntityFrameworkCore;
using Teatro.Core.Scenography;

namespace Teatro.EntityFrameworkCore;

public class TeatroDbContext : DbContext
{
    #region Documents

    public virtual DbSet<Scenography> Scenographies{ get; set; }

    #endregion
    
    public TeatroDbContext (DbContextOptions<TeatroDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        #region Documents

        modelBuilder.Entity<Scenography>(b =>
        {
            b.HasIndex(e => new { e.DocumentId }).IsUnique();
        });

        #endregion
        
    }
}