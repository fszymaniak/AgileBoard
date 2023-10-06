using AgileBoard.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace AgileBoard.Infrastructure.DAL;

internal sealed class AgileBoardDbContext : DbContext
{
    public DbSet<Epic> Epics { get; set; }

    public AgileBoardDbContext(DbContextOptions<AgileBoardDbContext> dbContextOptions) : base(dbContextOptions)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
    }
}