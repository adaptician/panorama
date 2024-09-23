using Microsoft.EntityFrameworkCore;
using Teatro.Core.Scenes;
using Teatro.Core.Scenography;

namespace Teatro.EntityFrameworkCore;

public class TeatroDbContext : DbContext
{
    #region Documents

    public virtual DbSet<Scenography> Scenographies{ get; set; }

    #endregion

    #region Scenes

    public virtual DbSet<Scene> Scenes { get; set; }
    public virtual DbSet<SceneTag> SceneTags { get; set; }

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
        
        #region Scenes

        modelBuilder.Entity<SceneTag>(b =>
        {
            b.HasIndex(e => new { e.Name, e.SceneId }).IsUnique();
        });

        #endregion
        
    }
}