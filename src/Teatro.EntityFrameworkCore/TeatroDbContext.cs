using Microsoft.EntityFrameworkCore;

namespace Teatro.EntityFrameworkCore;

public class TeatroDbContext : DbContext
{
    
    
    public TeatroDbContext (DbContextOptions<TeatroDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }
}